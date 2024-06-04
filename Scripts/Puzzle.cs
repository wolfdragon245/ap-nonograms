using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using FileAccess = Godot.FileAccess;

public partial class Puzzle : Control
{
	private GridContainer _boardRender;
	private HBoxContainer _horzRender;
	private VBoxContainer _vertRender;

	private PackedScene _cell;
	private PackedScene _number;
	
	private bool[,] _board;
	private List<List<int>> _vertNums;
	private List<List<int>> _horzNums;
	public String Title;
	public String Author;

	public override void _Ready()
	{
		_boardRender = GetNode<GridContainer>("Board");
		_horzRender = GetNode<HBoxContainer>("Horz Numbers");
		_vertRender = GetNode<VBoxContainer>("Vert Numbers");

		_cell = ResourceLoader.Load<PackedScene>("res://Scenes/nonogram_cell.tscn");
		_number = ResourceLoader.Load<PackedScene>("res://Scenes/number.tscn");
	}

	public void MakeBoard(String filePath)
	{
		ResetBoard();
		FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
		String line = file.GetLine();
		Title = line.Split("\"")[1];

		line = file.GetLine();
		Author = line.Split("\"")[1];

		line = file.GetLine();
		int width = int.Parse(line.Replace("width ", ""));
		line = file.GetLine();
		int height = int.Parse(line.Replace("height ", ""));

		_board = new bool[width, height];

		_vertNums = new List<List<int>>();
		while (true)
		{
			line = file.GetLine();
			if (line.Contains("rows"))
			{
				break;
			}
		}

		for (int i = 0; i < width; i++)
		{
			line = file.GetLine();
			String[] numbers = line.Split(",");
			List<int> nums = new List<int>();
			foreach (String number in numbers)
			{
				nums.Add(int.Parse(number));
			}
			_vertNums.Add(nums);
		}
		
		_horzNums = new List<List<int>>();
		while (true)
		{
			line = file.GetLine();
			if (line.Contains("columns"))
			{
				break;
			}
		}

		for (int i = 0; i < width; i++)
		{
			line = file.GetLine();
			String[] numbers = line.Split(",");
			List<int> nums = new List<int>();
			foreach (String number in numbers)
			{
				nums.Add(int.Parse(number));
			}
			_horzNums.Add(nums);
		}
		
		RenderBoard();
	}

	private void RenderBoard()
	{
		int longestHorz = 0;
		foreach (List<int> nums in _horzNums)
		{
			VBoxContainer column = new VBoxContainer();
			column.Alignment = BoxContainer.AlignmentMode.End;
			foreach (int num in nums)
			{
				RichTextLabel numDisp = _number.Instantiate<RichTextLabel>();
				numDisp.Text += num;
				column.AddChild(numDisp);
			}
			if (nums.Count > longestHorz)
			{
				longestHorz = nums.Count;
			}
			_horzRender.AddChild(column);
		}

		int longestVert = 0;
		foreach (List<int> nums in _vertNums)
		{
			HBoxContainer row = new HBoxContainer();
			row.Alignment = BoxContainer.AlignmentMode.End;
			foreach (int num in nums)
			{
				RichTextLabel numDisp = _number.Instantiate<RichTextLabel>();
				numDisp.Text += num;
				row.AddChild(numDisp);
			}
			if (nums.Count > longestVert)
			{
				longestVert = nums.Count;
			}
			_vertRender.AddChild(row);
		}
		
		_boardRender.Columns = _board.GetLength(0);
		for (int i = 0; i < _board.GetLength(0); i++)
		{
			for (int k = 0; k < _board.GetLength(1); k++)
			{
				NonogramCell cell = _cell.Instantiate<NonogramCell>();
				_boardRender.AddChild(cell);
			}
		}
		_boardRender.Position = new Vector2(longestVert*32, longestHorz*32);
		_vertRender.Position = new Vector2(0, longestHorz * 32);
		_horzRender.Position = new Vector2(longestVert * 32, 0);

		GetParent<Window>().Size =
			(Vector2I)_boardRender.Position + (new Vector2I(_board.GetLength(0), _board.GetLength(1)) * 32);
		GetParent<Window>().Visible = true;
	}

	private void ResetBoard()
	{
		GetParent<Window>().Visible = false;
		foreach (var child in _boardRender.GetChildren())
		{
			child.QueueFree();
		}
	}
}
