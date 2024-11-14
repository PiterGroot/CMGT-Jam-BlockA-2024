using Godot;
using System;

public partial class QuitButton : Button
{
	public override void _Ready()
	{
		this.Pressed += OnButtonPressed;
	}

	private void OnButtonPressed()
	{
		GetTree().Quit();
	}

	public override void _ExitTree()
	{
		this.Pressed -= OnButtonPressed;

		base._ExitTree();
	}
}
