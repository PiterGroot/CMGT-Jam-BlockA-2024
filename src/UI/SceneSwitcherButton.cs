using Godot;
using System.IO;

public partial class SceneSwitcherButton : Button
{
	[Export] private string _scenePath = "res://path/to/your/scene.tscn";

	public override void _Ready()
	{
		this.Pressed += OnButtonPressed;
	}

	private void OnButtonPressed()
	{
		var newScene = (PackedScene)GD.Load(_scenePath);

		if (newScene != null)
		{
			GetTree().ChangeSceneToPacked(newScene);
		}
		else
		{
			GD.PrintErr("Failed to load scene at path: " + _scenePath);
		}
	}

	public override void _ExitTree()
	{
		this.Pressed -= OnButtonPressed;

		base._ExitTree();
	}
}
