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
		if (Input.IsActionJustPressed("Send Message") && !_typeLine.Text.Equals(""))
		{
			Universal.Session.Say(_typeLine.Text);
			_typeLine.Text = "";
		}

		_typeLine.Size = new Vector2(GetParent<Window>().Size.X, _typeLine.Size.Y);
		_display.Size = new Vector2(GetParent<Window>().Size.X, GetParent<Window>().Size.Y - 31);
		_typeLine.Position = new Vector2(_typeLine.Position.X, _display.Size.Y);
	}

	public void PushMessage(LogMessage message)
	{
		_display.PushMessage(message);
	}

	public void PushMessage(String message)
	{
		_display.PushMessage(message);
	}

	public void ClearText()
	{
		_display.ClearMessages();
	}
}
