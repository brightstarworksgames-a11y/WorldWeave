using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 200.0f;

	[Export]
	public float SwimSpeed = 100.0f;

	private Terrain _terrain;

	public override void _Ready()
	{
		_terrain = GetParent().GetNode<Terrain>("Terrain");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector(
			"move_left",
			"move_right",
			"move_up",
            "move_down"
		);

		MovementType movementType =
			_terrain.GetMovementTypeAtGlobalPosition(GlobalPosition);

		float currentSpeed = movementType switch
		{
			MovementType.Swim => SwimSpeed,
			_ => Speed
		};

		Velocity = direction * currentSpeed;

		MoveAndSlide();
	}
}
