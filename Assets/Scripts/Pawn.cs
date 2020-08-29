using UnityEngine;
using System;

/// <summary>
/// Encapsulates an entity that is locked to the grid of a
/// battlemap and is targetable.
/// </summary>
public class Pawn : MonoBehaviour, IGridBased, IDefender
{
    /// <summary>
    /// Occurs when this pawn's health changes.
    /// </summary>
    public event Action<int> OnHealthChanged;

    /// <summary>
    /// Gets this pawns coordinate on the battlemap.
    /// </summary>
    public Vector2Int MapPosition { get; private set; }
    /// <summary>
    /// Gets the battlemap that this pawn is on.
    /// </summary>
    public BattleMap Map { get { return GetComponentInParent<BattleMap>(); } }
    /// <summary>
    /// Gets or sets this pawn's current health.
    /// </summary>
    public int Health
    {
        get { return health; }
        set
        {
            int delta = health - value;
            if (delta != 0)
            {
                health = value;
                OnHealthChanged?.Invoke(delta);
            }
        }
    }
    /// <summary>
    /// Gets this pawns defense.
    /// </summary>
    public float Defense { get; protected set; } = 0f;
    /// <summary>
    /// Gets this pawns evasion.
    /// </summary>
    public float Evasion { get; protected set; } = 0f;
    /// <summary>
    /// Gets the maximum health of this pawn.
    /// </summary>
    public int MaxHealth { get; protected set; } = 100;

    private int health = 100;

    protected virtual void Awake()
    {
        MapPosition = Map.WorldPositionToCoordinate((Vector2)transform.position);
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
