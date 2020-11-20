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
    /// Gets this pawns position in world space.
    /// </summary>
    public Vector2 WorldPosition { get { return transform.position; } }
    /// <summary>
    /// Gets this pawns coordinate on the grid.
    /// </summary>
    public Vector2Int GridPosition { get; private set; }
    /// <summary>
    /// Gets the formation this pawn is part of.
    /// </summary>
    public Formation Formation { get { return Grid as Formation; } }
    /// <summary>
    /// Gets the grid this pawn is on.
    /// </summary>
    public Grid Grid { get { return GetComponentInParent<Grid>(); } }
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
        Vector2Int gridPosition;
        bool onGrid = Grid.WorldPositionToCoordinate(transform.position, out gridPosition);

        if (onGrid)
            GridPosition = gridPosition;
        else
            Debug.LogError("Pawn not on grid!");
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
        GridPosition = coordinate;
        transform.position = Grid.CoordinateToWorldPosition(coordinate);
    }
}
