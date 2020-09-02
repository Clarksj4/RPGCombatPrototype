using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BattleMap : MonoBehaviour
{
    public Grid Grid { get { return grid; } }
    [SerializeField]
    private Grid grid;

    public UnityEvent<BattleMap, Vector2Int> OnInput;

    private Pawn[] pawns;

    private void Awake()
    {
        pawns = GetComponentsInChildren<Pawn>();
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
        return Mathf.Abs(delta.x) + Mathf.Abs(delta.y);
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
