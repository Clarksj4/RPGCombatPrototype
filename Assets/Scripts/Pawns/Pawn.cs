using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;
using System.Collections.Generic;

/// <summary>
/// Encapsulates an entity that is locked to the grid of a
/// battlemap and is targetable.
/// </summary>
public class Pawn : MonoBehaviour, IGridBased, IDefender, ITurnBased
{
    /// <summary>
    /// The minimum chance for an attack to hit.
    /// </summary>
    private const int MINIMUM_HIT_CHANCE = 10;

    /// <summary>
    /// Occurs when this pawn's health changes.
    /// </summary>
    public event Action<int> OnHealthChanged;

    [SerializeField] private PawnStats stats;

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
    public Formation Formation { get { return Cell.Formation; } }
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
    public int Defense { get; set; }
    /// <summary>
    /// Gets this pawns evasion.
    /// </summary>
    public int Evasion { get; set; }
    /// <summary>
    /// Gets the maximum health of this pawn.
    /// </summary>
    public int MaxHealth { get; set; }
    /// <summary>
    /// Gets the priority of this pawn in the turn order.
    /// </summary>
    public float Priority { get; set; }

    private List<PawnStatus> statuses = new List<PawnStatus>();

    private int health = 100;

    protected virtual void Awake()
    {
        stats.SetStats(this);
    }

    public void AddStatus(PawnStatus status)
    {
        status.Pawn = this;
        bool collated = statuses.Any(s => s.Collate(status));
        if (!collated)
            statuses.Add(status);
    }

    public void RemoveStatus(PawnStatus status)
    {
        statuses.Remove(status);
    }

    /// <summary>
    /// Moves the pawn to the given cell.
    /// </summary>
    public void SetCell(Cell cell)
    {
        if (cell != null)
        {
            transform.SetParent(cell.transform, true);
            transform.localPosition = Vector3.zero;
        }

        else
            transform.SetParent(null);
    }

    /// <summary>
    /// Moves the pawn to the cell at the given coordinate.
    /// </summary>
    public void SetCoordinate(Vector2Int coordinate)
    {
        Cell cell = Grid.GetCell(coordinate);
        SetCell(cell);
    }

    /// <summary>
    /// Moves this pawn to the given cell.
    /// </summary>
    public void Move(Cell cell)
    {
        if (cell != null)
        {
            // Get final position
            Vector3 targetWorldPosition = cell.WorldPosition;

            // Move actor to position over time
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(targetWorldPosition, 0.5f).SetEase(Ease.OutQuad));
            sequence.OnComplete(() => {
                // Update coordinate on arrival
                SetCell(cell);
            });
        }
    }

    /// <summary>
    /// Pushes this pawn relative to the given cell.
    /// </summary>
    public void TakePush(Cell origin, RelativeDirection direction, int amount)
    {
        // Get direction of push
        Vector2Int travelDirection = Coordinate.GetRelativeDirections(origin.Coordinate, direction).Single();
        TakePush(travelDirection, amount);
    }

    /// <summary>
    /// Pushes this pawn in the given direction by amount.
    /// </summary>
    public void TakePush(Vector2Int direction, int amount)
    {
        print($"push in direction: {direction}");

        Cell furthestUnoccupiedCell = null;
        for (int i = 1; i <= amount; i++)
        {
            // Get cell at coordinate
            Vector2Int coordinate = Coordinate + (direction * i);
            Cell cell = Grid.GetCell(coordinate);

            // If its empty mark it as a possible destination
            if (cell != null && !cell.IsOccupied())
                furthestUnoccupiedCell = cell;

            // If its not empty, pawn can't be pushed any further.
            else break;
        }

        // Move as far as possible
        Move(furthestUnoccupiedCell);
    }

    /// <summary>
    /// Swaps the positions of these two pawns.
    /// </summary>
    public void Swap(Pawn other)
    {
        Move(other.Cell);
        other.Move(Cell);
    }

    /// <summary>
    /// Tests whether the given attack hits this pawn.
    /// </summary>
    public bool IsHit(IAttacker attacker)
    {
        // Always have a minimum chance to hit
        float hitChance = 100 - Mathf.Max(MINIMUM_HIT_CHANCE, attacker.Accuracy - Evasion);

        // Roll to hit
        float roll = UnityEngine.Random.Range(0, 101);
        bool hit = roll >= hitChance;

        string hitString = hit ? "hits" : "misses";
        print($"{attacker.name} {hitString} {name} with roll: {roll} vs {hitChance}");

        return hit;
    }

    /// <summary>
    /// Deals damage to this pawn taking attack and defense
    /// values into account.
    public void TakeAttack(IAttacker attacker)
    {
        LoseHealth((int)(attacker.Attack - Defense));
    }

    /// <summary>
    /// Reduces this pawn's health by the given amount.
    /// </summary>
    public int LoseHealth(int amount)
    {
        int clamped = Mathf.Min(Health, amount);
        Health -= clamped;
        return clamped;
    }

    /// <summary>
    /// Increases this pawn's health by the given amount.
    /// </summary>
    public int GainHealth(int amount)
    {
        int clamped = Mathf.Min(amount, MaxHealth - Health);
        Health += clamped;
        return clamped;
    }

    public void Destroy()
    {
        SetCell(null);

        // Scale out the pawn then remove it.
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0, 0.5f).SetEase(Ease.InQuad));
        sequence.OnComplete(() => {
            Destroy(gameObject);
        });
    }

    protected virtual void LoadStats()
    {
        Defense = stats.Defense;
        Evasion = stats.Evasion;
        MaxHealth = stats.MaxHealth;
    }
}
