using Godot;

public enum MovementType
{
	Land,
	Swim
}

public partial class Terrain : TileMapLayer
{
	public MovementType GetMovementTypeAtGlobalPosition(Vector2 globalPosition)
	{
		Vector2 localPosition = ToLocal(globalPosition);
		Vector2I cellPosition = LocalToMap(localPosition);

		TileData tileData = GetCellTileData(cellPosition);

		if (tileData == null)
		{
			return MovementType.Land;
		}

		Variant terrainType = tileData.GetCustomData("terrain_type");

		if (terrainType.VariantType == Variant.Type.Nil)
		{
			return MovementType.Land;
		}

		return terrainType.AsString() switch
		{
			"sea" => MovementType.Swim,
			_ => MovementType.Land
		};
	}

	public Rect2 GetWorldBounds()
	{
		Rect2I usedRect = GetUsedRect();

		Vector2 tileSize = TileSet.TileSize;

		Vector2 topLeft = MapToLocal(usedRect.Position) - tileSize / 2.0f;
		Vector2 bottomRight =
			MapToLocal(usedRect.End - Vector2I.One) + tileSize / 2.0f;

		return new Rect2(
			ToGlobal(topLeft),
			ToGlobal(bottomRight) - ToGlobal(topLeft)
		);
	}
}
