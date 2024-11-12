using Godot;
using System;

public partial class ControlPanel : Node3D
{
	[Export] private Node3D leftLever;
	[Export] private Transform3D rightLever;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EventBus.Subscribe("move_forward", OnPitchUp);
		EventBus.Subscribe("move_backward", OnPitchDown);
	}

	private void OnPitchUp(object eventData)
	{
		leftLever.RotateX((float)(1 * GetProcessDeltaTime()));
	}

	private void OnPitchDown(object eventData)
	{
		leftLever.RotateX((float)(-1 * GetProcessDeltaTime()));
	}
}
