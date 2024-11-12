using Godot;
using System;

public partial class PlanetData : CsgSphere3D
{
	[Export] public float GravityStrength = 10.0f; 
	[Export] public float GravityRadius = 100.0f;

	public override void _Ready()
	{
		AddToGroup("planets");
	}
}
