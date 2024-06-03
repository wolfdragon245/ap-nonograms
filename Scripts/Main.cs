using Godot;
using System;
using APNonograms.Scripts;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;

public partial class Main : Node2D
{
	private Button _connect;
	private Button _disconnect;
	private LineEdit _ip;
	private LineEdit _slot;
	private LineEdit _password;
	private TextClient _textClient;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_connect = GetNode<Button>("Connect");
		_disconnect = GetNode<Button>("Disconnect");
		_ip = GetNode<LineEdit>("IP");
		_slot = GetNode<LineEdit>("Slot");
		_password = GetNode<LineEdit>("Password");
		
		_textClient = FindChild("Text Client") as TextClient;

		_connect.Pressed += () =>
		{
			Connect(_ip.Text, _slot.Text, _password.Text);
		};

		_disconnect.Pressed += () =>
		{
			Disconnect();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_connect.Visible = !Universal.Connected;
		_disconnect.Visible = Universal.Connected;
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
	}
	
	public void Disconnect()
	{
		Universal.Session.Socket.DisconnectAsync();
		_textClient.GetParent<Window>().Visible = false;
		Universal.Connected = false;
	}
}
