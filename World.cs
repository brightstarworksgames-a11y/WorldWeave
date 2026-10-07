using Godot;

public partial class World : Node2D
{
	private Terrain _terrain;
	private Camera2D _camera;

	public override void _Ready()
	{
		_terrain = GetNode<Terrain>("Terrain");
		_camera = GetNode<Camera2D>("Player/Camera2D");

		SetupCameraLimits();
	}

	private void SetupCameraLimits()
	{
		Rect2 worldBounds = _terrain.GetWorldBounds();

		_camera.LimitLeft = Mathf.RoundToInt(worldBounds.Position.X);
		_camera.LimitTop = Mathf.RoundToInt(worldBounds.Position.Y);
		_camera.LimitRight = Mathf.RoundToInt(worldBounds.End.X);
		_camera.LimitBottom = Mathf.RoundToInt(worldBounds.End.Y);
	}
}
