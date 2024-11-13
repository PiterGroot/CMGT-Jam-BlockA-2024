using Godot;

public partial class SpaceShipFuel : Node
{
	[Export] private float maxFuel = 100;
	[Export] private float moveFuelConsumption = 2f;

	public float CurrentFuel { get; private set; } = 0;

	public override void _Ready()
	{
		CurrentFuel = maxFuel;
	}

	public void OnMove()
	{
		CurrentFuel = (float)Mathf.Clamp(CurrentFuel - GetProcessDeltaTime() * moveFuelConsumption, 0, maxFuel);
	}
}
