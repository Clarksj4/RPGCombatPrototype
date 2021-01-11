using UnityEngine;
using System;
using System.Linq;

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
    public Vector2Int Coordinate { get { return Cell.Coordinate; } }
    /// <summary>
    /// Gets the cell this pawn occupies.
    /// </summary>
    public Cell Cell { get { return GetComponentInParent<Cell>(); } }
    /// <summary>
    /// Gets the formation this pawn is part of.
    /// </summary>
    public Formation Formation { get { return BattleManager.Instance.Formations.FirstOrDefault(f => f.Pawns.Contains(this)); } }
    /// <summary>
    /// Gets the direction this pawn is facing.
    /// </summary>
    public Vector2Int Facing { get { return Formation.Facing; } }
    /// <summary>
    /// Gets the grid this pawn is on.
    /// </summary>
    public MonoGrid Grid { get { return GetComponentInParent<MonoGrid>(); } }
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
                health = Mathf.Clamp(value, 0, MaxHealth);
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

    /// <summary>
    /// Sets the pawns world position without updating their map coordinate.
    /// </summary>
    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    /// <summary>
    /// Moves the pawn to the given cell.
    /// </summary>
    public void SetCell(Cell cell)
    {
        transform.SetParent(cell.transform, false);
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Moves the pawn to the cell at the given coordinate.
    /// </summary>
    public void SetCoordinate(Vector2Int coordinate)
    {
        Cell cell = Grid.GetCell(coordinate);
        SetCell(cell);
    }
}
