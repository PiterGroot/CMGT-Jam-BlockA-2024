using Godot;
using System;

public partial class PlanetRaycast : Camera3D
{
	[Export] private Node3D[] planets;
	[Export] private Label levelID;
	[Export] private string[] scenePaths;

	private int currentLevelID = 0;

	public override void _Process(double delta)
	{
		if (CheckIfMouseHoveringPlanet() && Input.IsMouseButtonPressed(MouseButton.Left)) 
		{
			GD.Print("PLAY SOUND EFFECT HERE");
			var newScene = (PackedScene)GD.Load(scenePaths[currentLevelID]);

			if (newScene != null)
				GetTree().ChangeSceneToPacked(newScene);
		}
	}

	private bool CheckIfMouseHoveringPlanet()
	{
		Vector2 mousePosition = GetViewport().GetMousePosition();
		var spaceState = GetWorld3D().DirectSpaceState;

		var from = ProjectRayOrigin(mousePosition);
		var to = from + ProjectRayNormal(mousePosition) * 1000;

		var ray = new PhysicsRayQueryParameters3D() { From = from, To = to, CollisionMask = (uint)1 };
		var result = spaceState.IntersectRay(ray);

		if (result.Count > 0)
		{
			Node3D hitObject = result["collider"].As<Node3D>();
			if (hitObject != null)
			{
				for (int i = 0; i < planets.Length; i++)
				{
					planets[i].Scale = Vector3.One;
				}

				currentLevelID = int.Parse(hitObject.Name.ToString());
				levelID.Text = "Level " + (currentLevelID + 1).ToString();

				hitObject.GetParentNode3D().Scale = (Vector3.One * 1.5f);

				return true;
			}
		}

		return false;
	}
}
