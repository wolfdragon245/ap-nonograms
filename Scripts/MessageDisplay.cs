using Godot;
using System;
using Archipelago.MultiClient.Net.MessageLog.Messages;

public partial class MessageDisplay : RichTextLabel
{
	public void PushMessage(LogMessage message)
	{
		CallDeferred("_pushMessage", message + "\n");
	}
	
	public void PushMessage(String message)
	{
		CallDeferred("_pushMessage", message + "\n");
	}

	public void ClearMessages()
	{
		Text = "[color=black]";
	}

	private void _pushMessage(Variant messgae)
	{
		AppendText(messgae.AsString());
	}
}
