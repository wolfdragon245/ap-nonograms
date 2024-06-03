using Godot;
using System;
using APNonograms.Scripts;
using Archipelago.MultiClient.Net.MessageLog.Messages;

public partial class TextClient : Control
{
	private MessageDisplay _display;
	private LineEdit _typeLine;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_display = GetNode<MessageDisplay>("Display");
		_typeLine = GetNode<LineEdit>("Type Line");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Send Message"))
		{
			Universal.Session.Say(_typeLine.Text);
			_typeLine.Text = "";
		}
	}

	public void PushMessage(LogMessage message)
	{
		_display.PushMessage(message);
	}
}
