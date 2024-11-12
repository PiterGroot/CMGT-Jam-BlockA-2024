using Godot;
using System;

public partial class ControlPanel : Node3D
{
	[Export] private Node3D leftLever;
	[Export] private Transform3D rightLever;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		leftLever.RotateX((float)Mathf.Sin(Time.GetTicksMsec() * delta));
	}
}
