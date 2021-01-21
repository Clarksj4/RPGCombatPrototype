using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;
using System.Collections.Generic;

/// <summary>
/// Encapsulates an entity that is locked to the grid of a
/// battlemap and is targetable.
/// </summary>
public class Pawn : MonoBehaviour, IGridBased, ITurnBased
{
    /// <summary>
    /// Occurs when this pawn's health changes.
    /// </summary>
    public event Action<int> OnHealthChanged;
    /// <summary>
    /// Occurs when this pawn defers damage to another.
    /// </summary>
    public event Action<Pawn, int> OnHealthLossDelegated;
    /// <summary>
    /// Occurs when the pawn is targeted by an ability. Whether
    /// the pawn is hit or not is passed as an argument.
    /// </summary>
    public event Action<bool> OnAttacked;
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
    /// Gets the rank this actor is in.
    /// </summary>
    public int Rank { get { return Formation.GetRank(Coordinate); } }
    /// <summary>
    /// Gets the file this actor is in.
    /// </summary>
    public int File { get { return Formation.GetFile(Coordinate); } }
    /// <summary>
    /// Gets or sets this pawn's current health.
    /// </summary>
    public int Health { get; set; }
    /// <summary>
    /// Gets this pawns defense.
    /// </summary>
    public int Defense { get; set; }
    /// <summary>
    /// Gets the maximum health of this pawn.
    /// </summary>
    public int MaxHealth { get; set; }
    /// <summary>
    /// Gets the priority of this pawn in the turn order.
    /// </summary>
    public float Priority { get; set; }
    /// <summary>
    /// Gets the damage amplification amount.
    /// </summary>
    public float Power { get; set; }
    /// <summary>
    /// Gets whether this pawn can be hit by abilities.
    /// </summary>
    public bool Evasive { get; set; }
    /// <summary>
    /// Gets whether this actor is currently able to take actions.
    /// </summary>
    public bool Incapacitated { get { return Sleeping || Stunned; } }
    /// <summary>
    /// Gets whether this actor is currently sleeping.
    /// </summary>
    public bool Sleeping { get; set; }
    /// <summary>
    /// Gets whether this actor is currently stunned.
    /// </summary>
    public bool Stunned { get; set; }
    /// <summary>
    /// Gets how far this actor can move in their turn.
    /// </summary>
    public int Movement { get; set; }
    /// <summary>
    /// Gets whether this actor is immune to damage from
    /// defendable sources.
    /// </summary>
    public bool Invulnerable { get; set; }
    
    [SerializeField]
    private PawnStats stats;
    private List<Pawn> surrogates = new List<Pawn>();
    private List<PawnStatus> statuses = new List<PawnStatus>();

    protected virtual void Awake()
    {
        stats.SetStats(this);

        // Add to turn order
        TurnManager.Instance.Add(this);
        TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
        TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;
    }

    /// <summary>
    /// Adds the given status to this pawn.
    /// </summary>
    public void AddStatus(PawnStatus status)
    {
        status.Pawn = this;
        bool collated = statuses.Any(s => s.Collate(status));
        if (!collated)
            statuses.Add(status);
    }

    /// <summary>
    /// Removes the given status from this pawn.
    /// </summary>
    public void RemoveStatus(PawnStatus status)
    {
        statuses.Remove(status);
    }

    /// <summary>
    /// Checks if this pawn is currently affected by
    /// a status of the given type.
    /// </summary>
    public bool HasStatus<T>() where T : PawnStatus
    {
        return statuses.Any(s => s is T);
    }

    /// <summary>
    /// Adds the given pawn as a surrogate for
    /// this pawn.
    /// </summary>
    public void AddSurrogate(Pawn pawn)
    {
        if (!IsSurrogate(pawn))
            surrogates.Add(pawn);
    }

    /// <summary>
    /// Removes the given pawn as a surrogate for
    /// this pawn.
    /// </summary>
    public void RemoveSurrogate(Pawn pawn)
    {
        surrogates.Remove(pawn);
    }

    /// <summary>
    /// Checks if this pawn has any surrogates.
    /// </summary>
    public bool HasSurrogate()
    {
        return surrogates.Any();
    }

    /// <summary>
    /// Checks if the given pawn is a surrogate for
    /// this pawn.
    /// </summary>
    public bool IsSurrogate(Pawn pawn)
    {
        return surrogates.Contains(pawn);
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
    public bool IsHit()
    {
        bool hit = !Evasive;
        OnAttacked?.Invoke(hit);
        return hit;
    }

    /// <summary>
    /// Deals damage to this pawn taking attack and defense
    /// values into account.
    public int TakeDamage(int amount, bool defendable = true)
    {
        int resolved = Mathf.Min(amount, Health);
        int healthDelta;
        
        // Can't defend against the damage, just take it
        if (!defendable)
            healthDelta = resolved;

        // CAN defend against damage, AND pawn is invulnerable
        // take NOTHING! muhahah!
        else if (Invulnerable)
            healthDelta = 0;

        // CAN defend...
        else
        {
            // Check for surrogates that will take damage on
            // behalf of this pawn.
            int amountResolved = 0;
            if (surrogates != null &&
                surrogates.Count > 0)
            {
                // Divide the damage between surrogates
                int div = amount / surrogates.Count;
                foreach (Pawn surrogate in surrogates)
                {
                    // Deal damage to surrogate
                    amountResolved += surrogate.TakeDamage(div);
                    OnHealthLossDelegated?.Invoke(surrogate, div);

                    // Update remaining damage to be dealt
                    div = (amount - amountResolved) / surrogates.Count;
                }
            }

            // Any remaining damage will be dealt to this pawn
            int remainder = amount - amountResolved;
            int inflicted = Mathf.Max(remainder - Defense, 0);
            int excess = Mathf.Max(inflicted - Health, 0);

            resolved = amount - excess;
            healthDelta = Mathf.Min(inflicted, Health);
        }

        // If any damage was actually inflicted
        if (healthDelta > 0)
        {
            Health -= healthDelta;
            OnHealthChanged?.Invoke(-healthDelta);
        }
            
        // Return the amount of damage this pawn abosrbed with 
        // health and or defense.
        return resolved;
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

    /// <summary>
    /// Sets the pawns health to the given value regardless of other factors.
    /// </summary>
    public void SetHealth(int value)
    {
        int clamped = Mathf.Clamp(value, 0, MaxHealth);
        int delta = clamped - Health;
        Health = clamped;
        OnHealthChanged?.Invoke(delta);
    }

    /// <summary>
    /// Destroys this pawn and tidies up all its bits.
    /// </summary>
    public void Destroy()
    {
        TurnManager.Instance.Remove(this);
        SetCell(null);

        // Scale out the pawn then remove it.
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0, 0.5f).SetEase(Ease.InQuad));
        sequence.OnComplete(() => {
            Destroy(gameObject);
        });
    }

    protected virtual void TurnStart()
    {
        // Automatically go to the next turn unless this
        // method is overridden.
        TurnManager.Instance.Next();
    }

    protected virtual void TurnEnd()
    {
        /* Nothing! */
    }

    private void HandleOnTurnStart(ITurnBased obj)
    {
        if (obj == (ITurnBased)this)
            TurnStart();
    }

    private void HandleOnTurnEnd(ITurnBased obj)
    {
        if (obj == (ITurnBased)this)
            TurnEnd();
    }
}
