using Godot;
using System;
using APNonograms.Scripts;

public partial class NonogramCell : TextureButton
{
	public int X;
	public int Y;
	private bool _moused;
	private bool _alreadyClicked;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MouseExited += () =>
		{
			_moused = false;
		};
		MouseEntered += () => _moused = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_moused && Input.IsActionPressed("Click") && !_alreadyClicked && !Disabled)
		{
			if (Universal.FirstCell)
			{
				Universal.FirstCell = false;
				Universal.Input = !ButtonPressed;
			}
			_alreadyClicked = true;
			ButtonPressed = Universal.Input;
			
		}

		if (_moused && Input.IsActionPressed("Mark") && !_alreadyClicked && !ButtonPressed)
		{
			if (Universal.FirstCell)
			{
				Universal.FirstCell = false;
				Universal.Input = !Disabled;
			}
			_alreadyClicked = true;
			Disabled = Universal.Input;
		}

		if (Input.IsActionJustReleased("Click") || Input.IsActionJustReleased("Mark"))
		{
			_alreadyClicked = false;
			Universal.FirstCell = true;
		}
	}
}
