using Godot;
using System;

public partial class FuelScreen : MeshInstance3D
{
	[Export]private ProgressBar progressBar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EventBus.Subscribe("fuel_update", OnFuelUpdated);
	}

	private void OnFuelUpdated(object eventData)
	{
		progressBar.Value = (float)eventData;
	}
}
