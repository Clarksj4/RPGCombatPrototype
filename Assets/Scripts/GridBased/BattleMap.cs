using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BattleMap : MonoBehaviour, IGridInputHandle
{
    public UnityEvent<BattleMap, Vector2Int> OnInput;

    private Grid grid;
    private Actor[] actors;

    private void Awake()
    {
        actors = GetComponentsInChildren<Actor>();
        grid = GetComponent<Grid>();
    }

    public Actor GetActorAtCoordinate(Vector2Int coordinate)
    {
        return actors.FirstOrDefault(a => a.MapPosition == coordinate);
    }

    public Vector2 GetWorldPositionFromCoordinate(Vector2Int coordinate)
    {
        return grid.CoordinateToWorldPosition(coordinate);
    }

    public void OnGridInput(Grid grid, Vector2Int coordinate)
    {
        OnInput?.Invoke(this, coordinate);
    }
}
