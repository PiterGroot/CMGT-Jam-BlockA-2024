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
		EventBus.Publish("fuel_update", CurrentFuel);

		if(CurrentFuel == 0)
		{
			SoundManager.Instance.player.Stream = SoundManager.Instance.death;
			SoundManager.Instance.player.Play();


			var newScene = (PackedScene)GD.Load("res://src/Resources/Scenes/LevelSelectMenu.tscn");
			if (newScene != null)
			{
				GetTree().ChangeSceneToPacked(newScene);
			}

		}
	}
}
