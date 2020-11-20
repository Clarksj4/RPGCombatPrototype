using UnityEngine;

public interface IGridBased
{
    /// <summary>
    /// Gets this actors coordinate on the battlemap.
    /// </summary>
    Vector2Int GridPosition { get; }
    /// <summary>
    /// Gets the grid that this actor is on.
    /// </summary>
    Grid Grid { get; }
    /// <summary>
    /// Moves the pawn to the given coordinate - also, updates their world position.
    /// </summary>
    void SetCoordinate(Vector2Int coordinate);
}
