using Godot;
using System;

public partial class PlanetSpin : Node3D
{
	[Export] private float spinSpeed = 5;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotateY((float)(spinSpeed * delta));
	}
}
