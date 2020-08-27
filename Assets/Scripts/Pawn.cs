using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour, IDefender
{
    /// <summary>
    /// Gets this actors coordinate on the battlemap.
    /// </summary>
    public Vector2Int MapPosition { get; private set; }
    /// <summary>
    /// Gets the battlemap that this actor is on.
    /// </summary>
    public BattleMap Map { get { return GetComponentInParent<BattleMap>(); } }
    /// <summary>
    /// Gets this pawn's current health.
    /// </summary>
    public int Health { get; set; } = 100;
    /// <summary>
    /// Gets this pawns defense.
    /// </summary>
    public float Defense { get; protected set; } = 0f;
    /// <summary>
    /// Gets this pawns evasion.
    /// </summary>
    public float Evasion { get; protected set; } = 0f;

    protected virtual void Awake()
    {
        MapPosition = Map.WorldPositionToCoordinate((Vector2)transform.position);
    }

    /// <summary>
    /// Reduces the pawns health by the given amount. Does NOT account for
    /// armour or other effects.
    /// </summary>
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }

    /// <summary>
    /// Sets the pawns world position without updating their map coordinate.
    /// </summary>
    public virtual void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    /// <summary>
    /// Moves the pawn to the given coordinate - also, updates their world position.
    /// </summary>
    public virtual void SetCoordinate(Vector2Int coordinate)
    {
        MapPosition = coordinate;
        transform.position = Map.CoordinateToWorldPosition(coordinate);
    }
}
