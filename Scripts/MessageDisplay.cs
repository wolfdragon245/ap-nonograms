using Godot;
using System;
using Archipelago.MultiClient.Net.MessageLog.Messages;

public partial class MessageDisplay : RichTextLabel
{
	public void PushMessage(LogMessage message)
	{
		CallDeferred("_pushMessage", message + "\n");
	}

	private void _pushMessage(Variant messgae)
	{
		AppendText(messgae.AsString());
	}
}
