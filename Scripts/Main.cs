using Godot;
using System;
using System.Linq;
using System.Net.Http;
using APNonograms.Scripts;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Newtonsoft.Json;
using HttpClient = System.Net.Http.HttpClient;

public partial class Main : Node2D
{
	private Button _connect;
	private Button _disconnect;
	private Button _new;
	private Button _check;
	private LineEdit _ip;
	private LineEdit _slot;
	private LineEdit _password;
	private TextClient _textClient;
	private Puzzle _puzzle;
	private String[] _puzzles;
	private bool _puzzlesReady;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_connect = GetNode<Button>("Connect");
		_disconnect = GetNode<Button>("Disconnect");
		_new = GetNode<Button>("New");
		_check = GetNode<Button>("Check");
		_ip = GetNode<LineEdit>("IP");
		_slot = GetNode<LineEdit>("Slot");
		_password = GetNode<LineEdit>("Password");
		
		_textClient = FindChild("Text Client") as TextClient;
		_puzzle = FindChild("Puzzle") as Puzzle;

		_connect.Pressed += () =>
		{
			Connect(_ip.Text, _slot.Text, _password.Text);
		};

		_disconnect.Pressed += () =>
		{
			Disconnect();
		};

		_new.Pressed += () =>
		{
			_puzzle.MakeBoard(SelectPuzzle());
		};

		_check.Pressed += () =>
		{
			if (_puzzle.CheckBoard())
			{
				_puzzle.GetParent<Window>().Title = _puzzle.Title + " By: " + _puzzle.Author;
				Hint();
			}
			else
			{
				_textClient.PushMessage("[color=red]Your solution is incorrect[/color]");
			}
		};

		GetPuzzles();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_connect.Visible = !Universal.Connected;
		_disconnect.Visible = Universal.Connected;

		_new.Visible = Universal.Connected && _puzzlesReady;
		_check.Visible = Universal.Connected && _puzzle.GetParent<Window>().Visible && !_puzzle.Solved;
	}
	
	public void Connect(String ip, String slot, String password)
	{
		Universal.Session = ArchipelagoSessionFactory.CreateSession(ip);
		Universal.Session.MessageLog.OnMessageReceived += message =>
		{
			_textClient.PushMessage(message);
		};
		
		LoginResult result = Universal.Session.TryConnectAndLogin(
			"",
			slot,
			ItemsHandlingFlags.NoItems,
			new Version(0, 5, 0),
			new[] {"TextOnly", "AP_Nonograms"},
			requestSlotData: false,
			password: password
		);

		if (result.Successful)
		{
			_textClient.GetParent<Window>().Visible = true;
		}
		Universal.Connected = result.Successful;
		_textClient.ClearText();
	}
	
	public void Disconnect()
	{
		Universal.Session.Socket.DisconnectAsync();
		_textClient.GetParent<Window>().Visible = false;
		_puzzle.GetParent<Window>().Visible = false;
		Universal.Connected = false;
	}

	public async void GetPuzzles()
	{
		using HttpResponseMessage data = await Universal.client.GetAsync("");
		data.EnsureSuccessStatusCode();
		_puzzles = JsonConvert.DeserializeObject<String[]>(await data.Content.ReadAsStringAsync());
		if (!DirAccess.DirExistsAbsolute("user://Puzzles/"))
		{
			DirAccess.MakeDirAbsolute("user://Puzzles/");
		}

		DirAccess dir = DirAccess.Open("user://Puzzles/");
		foreach (String file in dir.GetFiles())
		{
			dir.Remove(file);
		}

		foreach (String puzzle in _puzzles)
		{
			using HttpResponseMessage puzzleData = await Universal.client.GetAsync(puzzle.Replace(Universal.baseURL, ""));
			puzzleData.EnsureSuccessStatusCode();
			FileAccess file = FileAccess.Open("user://Puzzles/" + puzzle.Replace(Universal.baseURL + "Puzzles/", ""),
				FileAccess.ModeFlags.WriteRead);
			file.StoreLine(await puzzleData.Content.ReadAsStringAsync());
			file.Close();
		}

		_puzzles = dir.GetFiles();
		_puzzlesReady = true;
	}

	public String SelectPuzzle()
	{
		return "user://Puzzles/" + _puzzles[GD.RandRange(0, _puzzles.Length - 1)];
	}

	public void Hint()
	{
		var missing = Universal.Session.Locations.AllMissingLocations;
		var alreadyHinted = Universal.Session.DataStorage.GetHints()
			.Where(h => h.FindingPlayer == Universal.Session.ConnectionInfo.Slot)
			.Select(h => h.LocationId);

		var availableForHinting = missing.Except(alreadyHinted).ToArray();

		if (availableForHinting.Any())
		{
			var locationId = availableForHinting[GD.RandRange(0, availableForHinting.Length)];

			Universal.Session.Locations.ScoutLocationsAsync(true, locationId);
		}
	}
}
