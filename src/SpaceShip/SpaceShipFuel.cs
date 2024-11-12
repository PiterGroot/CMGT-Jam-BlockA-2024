using Godot;
using System;

public partial class SpaceShipFuel : Node
{
	[Export] private float maxFuel = 100;
	[Export] private float moveFuelConsumption = 1.5f;

	private float currentFuel = 0;

	public override void _Ready()
	{
		currentFuel = maxFuel;
	}

	public void OnMove()
	{
		currentFuel = (float)Mathf.Clamp(currentFuel - moveFuelConsumption * GetProcessDeltaTime(), 0, maxFuel);
	}
}
