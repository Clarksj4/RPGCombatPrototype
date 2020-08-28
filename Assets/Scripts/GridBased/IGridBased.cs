using UnityEngine;

public interface IGridBased
{
    /// <summary>
    /// Gets this actors coordinate on the battlemap.
    /// </summary>
    Vector2Int MapPosition { get; }
    /// <summary>
    /// Gets the battlemap that this actor is on.
    /// </summary>
    BattleMap Map { get; }
    /// <summary>
    /// Moves the pawn to the given coordinate - also, updates their world position.
    /// </summary>
    void SetCoordinate(Vector2Int coordinate);
}
