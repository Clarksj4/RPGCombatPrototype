using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Encapsulates an entity that is locked to the grid of a
/// battlemap and is targetable.
/// </summary>
[RequireComponent(typeof(StatSet))]
public class Pawn : MonoBehaviour, IGridBased, ITurnBased, ITeamBased, IStartable
{
    public PawnData Data;

    /// <summary>
    /// Occurs once THIS pawn has had its stats set.
    /// </summary>
    public UnityEvent<Pawn> OnInitialized;
    /// <summary>
    /// Occurs at the start of this pawn's turn after it
    /// has gained mana and statuses have been applied.
    /// </summary>
    public UnityEvent<Pawn> OnTurnStarted;
    /// <summary>
    /// Occurs at the start of this pawn's turn after it
    /// has gained mana and statuses have been applied.
    /// </summary>
    public UnityEvent<Pawn> OnTurnEnded;
    /// <summary>
    /// Occurs when this pawn's allegience changes.
    /// </summary>
    public UnityEvent<Pawn> OnTeamChanged;

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
    /// Gets the priority of this pawn in the turn order.
    /// </summary>
    public int Priority { get { return Stats["Priority"].Value; } }
    /// <summary>
    /// Gets whether this actor is currently able to take actions.
    /// </summary>
    public bool Incapacitated { get { return Stats.HasValue("Sleeping") || 
                                             Stats.HasValue("Stunned"); } }
    /// <summary>
    /// Gets whether this pawn has been setup successfully.
    /// </summary>
    public bool Initialized { get; private set; }
    /// <summary>
    /// Gets whether this pawn has abilities they can use.
    /// </summary>
    public bool IsActor { get { return Actions != null; } }
    /// <summary>
    /// Gets the stats of this pawn.
    /// </summary>
    public StatSet Stats { get; private set; }
    /// <summary>
    /// Gets the actions of this pawn.
    /// </summary>
    public ActionSet Actions { get; private set; }
    /// <summary>
    /// Gets the statuses on this pawn.
    /// </summary>
    public StatusSet Statuses { get; private set; }
    /// <summary>
    /// Gets or sets the team this pawn is allied with.
    /// </summary>
    public Team Team
    {
        get { return team; }
        set
        {
            team = value;
            OnTeamChanged?.Invoke(this);
        }
    }

    private Team team;
    private List<Pawn> surrogates = new List<Pawn>();
    private static int initOrder = 0;

    private void Awake()
    {
        Stats = GetComponent<StatSet>();
        Actions = GetComponent<ActionSet>();
        Statuses = GetComponent<StatusSet>();

        ActionManager.Instance.OnActionComplete += HandleOnActionComplete;
        PrioritizedStartManager.Instance.RegisterWithPriority(this, initOrder++);
    }

    public bool Initialize()
    {
        if (Data != null)
            Data.SetData(this);

        // Add to turn order
        TurnManager.Instance.Add(this);

        Initialized = true;
        OnInitialized?.Invoke(this);

        return true;
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
    /// Sets the pawn's parent cell to the given cell.
    /// </summary>
    public void SetCell(Cell cell, bool moveTo = false)
    {
        transform.SetParent(cell?.transform ?? null, true);

        if (moveTo)
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

    /// <summary>
    /// Moves this pawn to the given cell.
    /// </summary>
    public void Move(Cell cell)
    {
        if (cell != null)
        {
            SetCell(cell);

            // Get final position
            Vector3 targetWorldPosition = cell.WorldPosition;

            // Move actor to position over time
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(targetWorldPosition, 0.5f).SetEase(Ease.OutQuad));
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
            
            // If its empty and part of this formation, mark it as a possible destination
            if (cell != null && cell.Formation == Formation && !cell.IsOccupied())
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
        bool hit = !Stats.HasValue("Evasive");
        OnAttacked?.Invoke(hit);
        return hit;
    }

    /// <summary>
    /// Amplifies the given attack by this pawn's power stat.
    /// </summary>
    public int GetAmplifiedDamage(int baseDamage)
    {
        float factor = Stats["Power"].Value / 100f;
        float amplifiedDamage = baseDamage + (baseDamage * factor);
        int rounded = Mathf.RoundToInt(amplifiedDamage);
        return rounded;
    }

    /// <summary>
    /// Deals damage to this pawn taking attack and defense
    /// values into account.
    public int TakeDamage(int amount, bool defendable = true)
    {
        int resolved = Mathf.Min(amount, Stats["Health"].Value);
        int healthDelta = 0;
        
        // Can't defend against the damage, just take it
        if (!defendable)
            healthDelta = resolved;

        // CAN defend against damage, AND pawn is invulnerable
        // take NOTHING! muhahah!
        else if (Stats.HasValue("Invulnerable"))
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

                    // Update remaining damage to be dealt
                    div = (amount - amountResolved) / surrogates.Count;
                }
            }

            // Any remaining damage will be dealt to this pawn
            int remainder = amount - amountResolved;
            int inflicted = Mathf.Max(remainder - Stats["Defense"].Value, 0);
            int excess = Mathf.Max(inflicted - Stats["Health"].Value, 0);

            resolved = amount - excess;
            healthDelta = Mathf.Min(inflicted, Stats["Health"].Value);
        }

        // Apply change to health.
        Stats["Health"].Value -= healthDelta;
            
        // Return the amount of damage this pawn abosrbed with 
        // health and or defense.
        return resolved;
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

    public void OnTurnStart()
    {
        // Each pawn gains one mana at the start of their turn.
        if (!Stats.HasValue("Stunned"))
            Stats["Mana"]?.Increment(1);

        OnTurnStarted?.Invoke(this);

        // If actor is incapacitated, it doesn't get a turn.
        // Hard luck, bud.
        if (!CanAct())
            TurnManager.Instance.RequestTurnEnd();
    }

    public void OnTurnEnd()
    {
        OnTurnEnded?.Invoke(this);
    }

    private bool CanAct()
    {
        // Mustn't be incapacitated AND must have some actions left to use.
        if (IsActor)
            return !Incapacitated && Actions.AnyAvailableAction();

        // Not an actor
        return false;
    }

    private void HandleOnActionComplete(Pawn pawn, BattleAction action)
    {
        if (pawn == this && !CanAct())
            TurnManager.Instance.RequestTurnEnd();
    }
}
