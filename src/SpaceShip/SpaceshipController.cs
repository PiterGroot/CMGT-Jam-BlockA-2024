using Godot;


public partial class SpaceshipController : CharacterBody3D
{
	[Export] public float Speed = 10f;
	[Export] public float RotationSpeed = 2f;
	[Export] public float Acceleration = 1f;
	[Export] public float MaxSpeed = 50f;
	[Export] public float RotationSmoothing = 5f;
	[Export] public float CollisionRadius = 100f;

	[Export] private SpaceShipFuel SpaceShipFuel;

	private float _currentSpeed = 0f;
	private Vector3 _velocity = Vector3.Zero;
	private Vector3 _rotationVelocity = Vector3.Zero;

	private PlanetData _nearestPlanet = null;

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
		HandleRotation(delta);

		_velocity = -Transform.Basis.Z * _currentSpeed;
		
		ApplyGravity(delta);
		
		Velocity = _velocity;

		if (Mathf.Abs(_currentSpeed) > 1f) SpaceShipFuel.OnMove();
		MoveAndSlide();
	}

	private void HandleMovement(double delta)
	{
		if (Input.IsActionPressed("move_forward") && SpaceShipFuel.CurrentFuel > 0)
		{
			if (Engine.GetFramesDrawn() % 5 == 0)
			{
				SoundManager.Instance.player.Stream = SoundManager.Instance.thruster;
				SoundManager.Instance.player.Play();
			}

			_currentSpeed = Mathf.Min(_currentSpeed + (float)(Acceleration * delta), MaxSpeed);
			EventBus.Publish("move_forward");
		}
		else if (Input.IsActionPressed("move_backward") && SpaceShipFuel.CurrentFuel > 0)
		{
			if (Engine.GetFramesDrawn() % 5 == 0)
			{
				SoundManager.Instance.player.Stream = SoundManager.Instance.back;
				SoundManager.Instance.player.Play();
			}
			
			_currentSpeed = Mathf.Max(_currentSpeed - (float)(Acceleration * delta), -MaxSpeed);
			EventBus.Publish("move_backward");
		}
		else
			_currentSpeed = Mathf.Lerp(_currentSpeed, 0, (float)(delta * .5f));

			_currentSpeed = Mathf.Clamp(_currentSpeed, -MaxSpeed, MaxSpeed);
	}

	private void HandleRotation(double delta)
	{
		Vector3 rotationInput = Vector3.Zero;

		if (Input.IsActionPressed("pitch_up"))
			rotationInput.X += 1;
		if (Input.IsActionPressed("pitch_down"))
			rotationInput.X -= 1;

		if (Input.IsActionPressed("yaw_left"))
			rotationInput.Y += 1;
		if (Input.IsActionPressed("yaw_right"))
			rotationInput.Y -= 1;

		if (Input.IsActionPressed("roll_left"))
			rotationInput.Z += 1;
		if (Input.IsActionPressed("roll_right"))
			rotationInput.Z -= 1;

		_rotationVelocity = _rotationVelocity.Lerp(rotationInput * RotationSpeed, RotationSmoothing * (float)delta);

		RotateObjectLocal(Vector3.Right, _rotationVelocity.X * (float)delta);
		RotateObjectLocal(Vector3.Up, _rotationVelocity.Y * (float)delta);
		RotateObjectLocal(Vector3.Forward, _rotationVelocity.Z * (float)delta);
	}

	private void UpdateNearestPlanet()
	{
		float closestDistance = float.MaxValue;
		_nearestPlanet = null;

		foreach (Node node in GetTree().GetNodesInGroup("planets"))
		{
			if (node is PlanetData planet)
			{
				float distance = GlobalTransform.Origin.DistanceTo(planet.GlobalTransform.Origin);

				// Check if within the planet's gravitational radius and if it's the closest one
				if (distance < planet.GravityRadius && distance < closestDistance)
				{
					closestDistance = distance;
					_nearestPlanet = planet;
				}
			}
		}
	}
	
	private void ApplyGravity(double delta)
	{
		UpdateNearestPlanet();  // Find the nearest planet within range
		
		if (_nearestPlanet != null)
		{
			Vector3 directionToPlanet = (_nearestPlanet.GlobalTransform.Origin - GlobalTransform.Origin).Normalized();
			Vector3 gravitationalPull = directionToPlanet * _nearestPlanet.GravityStrength * (float)delta;

			// Apply gravitational pull to the spaceship's velocity
			_velocity += gravitationalPull;

		}
	}
}
