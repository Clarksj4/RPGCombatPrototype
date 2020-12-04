using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Encapsulates any action / ability used by an actor
/// during a battle.
/// </summary>
public abstract class BattleAction
{
    /// <summary>
    /// Gets the range of this action.
    /// </summary>
    public virtual int Range { get { return -1; } }
    /// <summary>
    /// Gets the actor who will perform this action.
    /// </summary>
    public Actor Actor { get; protected set; }
    /// <summary>
    /// Gets the map that this action is targeted to.
    /// </summary>
    public Formation TargetFormation { get; protected set; }
    /// <summary>
    /// Gets the coordinate that this action is targeting.
    /// </summary>
    public Vector2Int TargetPosition { get; protected set; }
    /// <summary>
    /// Gets the map where this action originates from.
    /// </summary>
    public Formation OriginFormation { get; protected set; }
    /// <summary>
    /// Gets the coordinate that this action originates from.
    /// </summary>
    public Vector2Int OriginPosition { get; protected set; }
    /// <summary>
    /// Gest a collection of informative tags about this action.
    /// </summary>
    public abstract ActionTag Tags { get; }
    /// <summary>
    /// Gets the applicable targets for this ability.
    /// </summary>
    public abstract TargetableCellContent TargetableCellContent { get; }
    /// <summary>
    /// Gets whether this action can target a formation other than
    /// the one the actor is currently on.
    /// </summary>
    public virtual TargetableFormation TargetableFormation { get { return TargetableFormation.Self; } }
    /// <summary>
    /// Gets the strategy for selecting which cells are targetable.
    /// </summary>
    protected TargetableStrategy targetableStrategy;
    /// <summary>
    /// Gets the collection of restrictions on cells that can be targeted.
    /// </summary>
    protected List<TargetableCellRestriction> targetRestrictions;
    /// <summary>
    /// Gets the strategy for selecting which cells are affected based
    /// upon a targeted cell.
    /// </summary>
    protected TargetedStrategy targetedStrategy;
    /// <summary>
    /// The sequence of things this battle action with do.
    /// </summary>
    protected List<ActionNode> actionSequence;

    public BattleAction()
    {
        // Default strategies
        targetableStrategy = new AnyCells(this);
        targetedStrategy = new TargetedPoint(this);
        targetRestrictions = new List<TargetableCellRestriction>()
        {
            new RangeRestriction(this),
            new CellContentRestriction(this, TargetableCellContent)
        };
    }

    /// <summary>
    /// Sets the actor who will perform this action. Returns
    /// true if the actor is able to perform the action.
    /// </summary>
    public virtual bool SetActor(Actor actor)
    {
        // Check if actor is able to do action.
        bool isAble = IsActorAble(actor);
        if (isAble)
        {
            // Set actor and originating map / position.
            Actor = actor;
            OriginFormation = actor.Formation;
            OriginPosition = actor.GridPosition;
        }
        return isAble;
    }

    /// <summary>
    /// Sets the target for this action if valid. Returns true
    /// if the target is a valid one.
    /// </summary>
    public virtual void SetTarget(Formation formation, Vector2Int position)
    {
        // Set target map / position
        TargetFormation = formation;
        TargetPosition = position;
    }

    /// <summary>
    /// Removes the action's current target.
    /// </summary>
    public void DeselectTarget()
    {
        TargetFormation = null;
        TargetPosition = Vector2Int.zero;
    }

    /// <summary>
    /// Gets all the formations that are valid targets for
    /// this action.
    /// </summary>
    public IEnumerable<Formation> GetTargetableFormations()
    {
        return BattleManager.Instance.Formations.Where(IsTargetFormationValid);
    }

    /// <summary>
    /// Gets all the cells that can be targeted by this action.
    /// </summary>
    public virtual IEnumerable<(Formation, Vector2Int)> GetTargetableCells()
    {
        IEnumerable<(Formation, Vector2Int)> cells = targetableStrategy.GetTargetableCells();

        // Return all cells that meet restrictions
        if (targetRestrictions != null && targetRestrictions.Count > 0)
        {
            foreach ((Formation formation, Vector2Int coordinate) in cells)
            {
                if (targetRestrictions.All(r => r.IsTargetValid(formation, coordinate)))
                    yield return (formation, coordinate);
            }
        }

        // Return all cells - no restrictions
        else
        {
            foreach ((Formation formation, Vector2Int coordinate) in cells)
                yield return (formation, coordinate);
        }
    }

    public virtual bool IsTargetFormationValid(Formation formation)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (TargetableFormation == TargetableFormation.All)  return true;
        if (TargetableFormation == TargetableFormation.None) return false;

        // Assume the formation is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        bool isSelfFormation = formation == Actor.Formation;

        // Can target own formation.
        if (TargetableFormation.HasFlag(TargetableFormation.Self) &&
            isSelfFormation)
            valid = true;

        // Can target other formations.
        else if (TargetableFormation.HasFlag(TargetableFormation.Other) &&
            !isSelfFormation)
            valid = true;

        return valid;
    }

    /// <summary>
    /// Checks if the given actor is currently able to perform
    /// this action.
    /// </summary>
    public virtual bool IsActorAble(Actor actor)
    {
        return !actor.Incapacitated;
    }

    /// <summary>
    /// Gets whether the assigned actor is able to complete
    /// the action with its current parameters.
    /// </summary>
    public virtual bool CanDo()
    {
        return IsActorAble(Actor);
    }

    /// <summary>
    /// Gets the coordinates that will be affected by this action.
    /// </summary>
    public IEnumerable<(Formation, Vector2Int)> GetAffectedCoordinates()
    {
        return targetedStrategy.GetAffectedCoordinates();
    }

    /// <summary>
    /// Performs this action. Returns true if the action was
    /// successful.
    /// </summary>
    public virtual IEnumerator Do()
    {
        foreach ((Formation formation, Vector2Int coordinate) in GetAffectedCoordinates())
        {
            // Apply action sequence to each affected cell.
            foreach (ActionNode action in actionSequence)
            {
                bool success = action.ApplyToCell(formation, coordinate);
                if (!success)
                    break;
            }
        }

        return null;
    }
}
