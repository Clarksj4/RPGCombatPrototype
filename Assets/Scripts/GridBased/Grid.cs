using System;
using UnityEngine;

[Serializable]
public class Grid
{
    public Vector2Int NCells { get { return nCells; } }
    public Vector2 CellSize { get { return cellSize; } }
    public Vector2 WorldPosition { get { return worldPosition; } }
    public Bounds Bounds { get { return new Bounds(WorldPosition, nCells * cellSize); } }

    [SerializeField]
    private Vector2Int nCells;
    [SerializeField]
    private Vector2 cellSize;
    [SerializeField]
    private Vector2 worldPosition;

    public Grid(Vector2 worldPosition, Vector2Int nCells, Vector2 cellSize)
    {
        this.worldPosition = worldPosition;
        this.nCells = nCells;
        this.cellSize = cellSize;
    }

    public Vector2 CoordinateToWorldPosition(Vector2Int coordinate)
    {
        return (Vector2)Bounds.min + (coordinate * CellSize) + (CellSize * 0.5f);
    }

    public bool WorldPositionToCoordinate(Vector2 worldPosition, out Vector2Int coordinate)
    {
        Vector2 delta = worldPosition - (Vector2)Bounds.min;
        Vector2 unboundedCoordinate = delta / CellSize;
        Vector2Int roundedUnboundedCoordinate = new Vector2Int((int)unboundedCoordinate.x, (int)unboundedCoordinate.y);

        coordinate = roundedUnboundedCoordinate;

        return ContainsCoordinate(coordinate);
    }

    public bool ContainsCoordinate(Vector2Int coordinate)
    {
        return coordinate.x >= 0 &&
                coordinate.x <= (NCells.x - 1) &&
                coordinate.y >= 0 &&
                coordinate.y <= (NCells.y - 1);
    }
}
