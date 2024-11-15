using Godot;
using System;

public partial class PlanetSpin : Node3D
{
	[Export] private float spinSpeed = 5;
	[Export] private bool renderGravityField = true;

	public override void _Ready()
	{
		if (!renderGravityField)
		{
			HideChildNode("GravityField");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotateY((float)(spinSpeed * delta));
	}

	private void HideChildNode(string childName)
	{
		Node child = FindChild(childName, recursive: true);
		if (child is CanvasItem canvasItem)
		{
			canvasItem.Visible = false; 
		}
		else if (child is Node3D node3D)
		{
			node3D.Hide(); 
		}
		else
		{
			GD.Print("Child node not found or not a supported type");
		}
	}
}
