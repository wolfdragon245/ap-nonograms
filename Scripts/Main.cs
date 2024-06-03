using Godot;
using System;
using APNonograms.Scripts;

public partial class Main : Node2D
{
	private Button _connect;
	private Button _disconnect;
	private LineEdit _ip;
	private LineEdit _slot;
	private LineEdit _password;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_connect = GetNode<Button>("Connect");
		_disconnect = GetNode<Button>("Disconnect");
		_ip = GetNode<LineEdit>("IP");
		_slot = GetNode<LineEdit>("Slot");
		_password = GetNode<LineEdit>("Password");

		_connect.Pressed += () =>
		{
			Universal.Connect(_ip.Text, _slot.Text, _password.Text);
		};

		_disconnect.Pressed += () =>
		{
			Universal.Disconnect();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_connect.Visible = !Universal.Connected;
		_disconnect.Visible = Universal.Connected;
	}
}
