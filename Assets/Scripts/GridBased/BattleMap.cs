using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BattleMap : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    public UnityEvent<BattleMap, Vector2Int> OnInput;

    private Pawn[] pawns;

    private void Awake()
    {
        pawns = GetComponentsInChildren<Pawn>();
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x <= grid.NCells.x; x++)
        {
            Vector2 from = new Vector2(grid.Bounds.min.x + (x * grid.CellSize.x), grid.Bounds.min.y);
            Vector2 to = from + (Vector2.up * grid.CellSize * grid.NCells.y);
            Gizmos.DrawLine(from, to);
        }

        for (int y = 0; y <= grid.NCells.y; y++)
        {
            Vector2 from = new Vector2(grid.Bounds.min.x, grid.Bounds.min.y + (y * grid.CellSize.y));
            Vector2 to = from + (Vector2.right * grid.CellSize * grid.NCells.x);
            Gizmos.DrawLine(from, to);
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (grid.WorldPositionToCoordinate(mousePosition, out var coordinate))
            OnInput?.Invoke(this, coordinate);
    }

    public bool IsInRange(Vector2Int from, Vector2Int to, int maxRange, int minRange = 0)
    {
        int distance = GetDistance(from, to);
        return distance <= maxRange && distance >= minRange;
    }

    public int GetDistance(Vector2Int from, Vector2Int to)
    {
        Vector2Int delta = to - from;
        return Mathf.Abs(delta.x + delta.y);
    }

    public Pawn GetPawnAtCoordinate(Vector2Int coordinate)
    {
        return pawns.FirstOrDefault(a => a.MapPosition == coordinate);
    }

    public Vector2 CoordinateToWorldPosition(Vector2Int coordinate)
    {
        return grid.CoordinateToWorldPosition(coordinate);
    }

    public Vector2Int WorldPositionToCoordinate(Vector2 position)
    {
        if (grid.WorldPositionToCoordinate(position, out var coordinate))
            return coordinate;

        throw new ArgumentException("World position not within grid bounds");
    }
}
