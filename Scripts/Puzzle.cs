using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using APNonograms.Scripts;
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
	public bool Solved;

	public override void _Ready()
	{
		_boardRender = GetNode<GridContainer>("Board");
		_horzRender = GetNode<HBoxContainer>("Horz Numbers");
		_vertRender = GetNode<VBoxContainer>("Vert Numbers");

		_cell = ResourceLoader.Load<PackedScene>("res://Scenes/nonogram_cell.tscn");
		_number = ResourceLoader.Load<PackedScene>("res://Scenes/number.tscn");
	}

	public void MakeBoard(String puzzle)
	{
		ResetBoard();
		FileAccess file = FileAccess.Open(puzzle, FileAccess.ModeFlags.Read);
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

		_vertRender.Size = new Vector2(longestVert * 32, _vertRender.Size.Y);
		_horzRender.Size = new Vector2(_horzRender.Size.X, longestHorz * 32);

		GetParent<Window>().Size =
			(Vector2I)_boardRender.Position + (new Vector2I(_board.GetLength(0), _board.GetLength(1)) * 32);
		GetParent<Window>().Title = "????? By: " + Author;
		GetParent<Window>().Visible = true;
	}

	private void ResetBoard()
	{
		GetParent<Window>().Visible = false;
		Solved = false;
		foreach (var child in _boardRender.GetChildren())
		{
			child.QueueFree();
		}

		foreach (var child in _horzRender.GetChildren())
		{
			child.Free();
		}
		
		foreach (var child in _vertRender.GetChildren())
		{
			child.Free();
		}

		if (_horzNums != null && _vertNums != null)
		{
			_horzNums.Clear();
			_vertNums.Clear();
		}
		_horzRender.Position = Vector2.Zero;
		_vertRender.Position = Vector2.Zero;
	}

	public bool CheckBoard()
	{
		_getBoard();
		bool valid = true;
		for (int i = 0; i < _board.GetLength(0); i++)
		{
			List<int> nums = new List<int>();
			bool counting = false;
			int num = 0;
			for (int k = 0; k < _board.GetLength(1); k++)
			{
				if (counting && _board[i, k])
				{
					num++;
				}
				
				if (!counting && _board[i, k])
				{
					counting = true;
					num++;
				}

				if (counting && !_board[i, k])
				{
					counting = false;
					nums.Add(num);
					num = 0;
				}
			}

			if (num > 0)
			{
				nums.Add(num);
			}

			List<int> checkNums = _vertNums[i];
			checkNums.Reverse();
			if (!_checkLists(nums, checkNums))
			{
				valid = false;
			}
		}
		
		for (int i = 0; i < _board.GetLength(1); i++)
		{
			List<int> nums = new List<int>();
			bool counting = false;
			int num = 0;
			for (int k = 0; k < _board.GetLength(0); k++)
			{
				if (counting && _board[k, i])
				{
					num++;
				}
				
				if (!counting && _board[k, i])
				{
					counting = true;
					num++;
				}

				if (counting && !_board[k, i])
				{
					counting = false;
					nums.Add(num);
					num = 0;
				}
			}

			if (num > 0)
			{
				nums.Add(num);
			}

			List<int> checkNums = _horzNums[i];
			if (!_checkLists(nums, checkNums))
			{
				valid = false;
			}
		}

		Solved = valid;
		return valid;
	}

	private void _getBoard()
	{
		int x = 0;
		int y = 0;
		for (int i = 0; i < _boardRender.GetChildCount(); i++)
		{
			_board[y, x] = _boardRender.GetChild<NonogramCell>(i).ButtonPressed;
			x++;
			if (x == _board.GetLength(0))
			{
				y++;
				x = 0;
			}
		}
		_printBoard();
	}

	private void _printBoard()
	{
		String toPrint;
		for (int i = 0; i < _board.GetLength(0); i++)
		{
			toPrint = "";
			for (int k = 0; k < _board.GetLength(1); k++)
			{
				if (_board[i,k])
				{
					toPrint += "#";
				}
				else
				{
					toPrint += "O";
				}
			}
			GD.Print(toPrint);
		}
	}

	private bool _checkLists(List<int> list1, List<int> list2)
	{
		if (list1.Count != list2.Count) return false;
		bool valid = true;
		for (int i = 0; i < list1.Count; i++)
		{
			if (list1[i] != list2[i])
			{
				valid = false;
			}
		}

		return valid;
	}
}
