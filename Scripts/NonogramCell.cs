using Godot;
using System;

public partial class NonogramCell : TextureButton
{
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
		if (_moused && Input.IsActionPressed("Click") && !_alreadyClicked)
		{
			_alreadyClicked = true;
			ButtonPressed = !ButtonPressed;
		}

		if (_moused && Input.IsActionPressed("Mark") && !_alreadyClicked)
		{
			_alreadyClicked = true;
			Disabled = !Disabled;
		}

		if (Input.IsActionJustReleased("Click") || Input.IsActionJustReleased("Mark"))
		{
			_alreadyClicked = false;
		}
	}
}
