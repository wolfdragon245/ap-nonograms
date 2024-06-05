using Godot;
using System;
using APNonograms.Scripts;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.MessageLog.Parts;

public partial class TextClient : Control
{
	private MessageDisplay _display;
	private LineEdit _typeLine;
	private ColorRect _bg;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_display = GetNode<MessageDisplay>("Display");
		_typeLine = GetNode<LineEdit>("Type Line");
		_bg = GetNode<ColorRect>("BG");
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
		_bg.Size = new Vector2(GetParent<Window>().Size.X, GetParent<Window>().Size.Y);
	}

	public void PushMessage(LogMessage message)
	{
		MessagePart[] parts = message.Parts;
		String toSend = "";
		foreach (MessagePart part in parts)
		{
			toSend += "[color=" + String.Format("{0:X2}{1:X2}{2:X2}", part.Color.R, part.Color.G, part.Color.B) + "]" + part.Text;
		}
		_display.PushMessage(toSend);
	}

	public void PushMessage(String message)
	{
		_display.PushMessage(message);
	}

	public void ClearText()
	{
		_display.Text = "";
	}
}
