using Godot;
using System;

public partial class SoundManager : Node
{
	public static SoundManager Instance { get; private set; }

	public AudioStreamPlayer player;

	public AudioStream button = GD.Load<AudioStream>("res://src/button.wav");
	public AudioStream thruster = GD.Load<AudioStream>("res://src/thruster.wav");
	public AudioStream back = GD.Load<AudioStream>("res://src/back.wav");
	public AudioStream death = GD.Load<AudioStream>("res://src/death.wav");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		player = new AudioStreamPlayer();
		AudioStream audioStream = button;

		player.Stream = audioStream;
		player.VolumeDb = -15;

		AddChild(player);
	}
}
