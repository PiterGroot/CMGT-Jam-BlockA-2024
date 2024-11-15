using Godot;
using System;

public partial class WinCondition : Node
{
	[Export] public string NextScenePath = "res://path/to/your/scene.tscn";

	public void TriggerWin()
	{
		if (!string.IsNullOrEmpty(NextScenePath))
		{
			var newScene = (PackedScene)GD.Load(NextScenePath);
			
			if (newScene != null)
			{
				GetTree().ChangeSceneToPacked(newScene);
			}
			else
			{
				GD.PrintErr("Failed to load scene at path: " + NextScenePath);
			}
		}
		else
		{
			GD.PrintErr("NextScenePath is not set!");
		}
	}
}
