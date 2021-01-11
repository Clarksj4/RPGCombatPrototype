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
    //
    // Setting up current target
    //

    /// <summary>
    /// Gets the grid this action is performed on.
    /// </summary>
    public MonoGrid Grid { get { return BattleManager.Instance.Grid; } }
    /// <summary>
    /// Gets the actor who will perform this action.
    /// </summary>
    public Actor Actor { get; protected set; }
    /// <summary>
    /// Gets the cell that this action is targeting.
    /// </summary>
    public Cell TargetCell { get; private set; }
    /// <summary>
    /// Gets the cell that this action originates from.
    /// </summary>
    public Cell OriginCell { get { return Actor.Cell; } }

    //
    // Target Validation.
    //

    /// <summary>
    /// Gets whether this action can target a formation other than
    /// the one the actor is currently on.
    /// </summary>
    protected TargetableFormation targetableFormation = TargetableFormation.None;
    /// <summary>
    /// Gets the strategy for selecting which cells are targetable.
    /// </summary>
    protected TargetableStrategy targetableStrategy = null;
    /// <summary>
    /// Gets the collection of restrictions on cells that can be targeted.
    /// </summary>
    protected List<TargetingRestriction> targetRestrictions = null;
    /// <summary>
    /// Gets the strategy for selecting which cells are affected based
    /// upon a targeted cell.
    /// </summary>
    protected TargetedStrategy targetedStrategy = null;
    /// <summary>
    /// The sequence of things this battle action with do.
    /// </summary>
    protected List<ActionNode> actionSequence = null;

    //
    // Action params
    //

    /// <summary>
    /// Gets the range of this action.
    /// </summary>
    public virtual int Range { get { return -1; } }
    /// <summary>
    /// Gest a collection of informative tags about this action.
    /// </summary>
    public ActionTag Tags { get; protected set; } = ActionTag.None;

    //
    // Methods
    //

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
        }
        return isAble;
    }

    /// <summary>
    /// Sets the target for this action if valid. Returns true
    /// if the target is a valid one.
    /// </summary>
    public virtual bool SetTarget(Cell cell)
    {
        bool validTarget = IsTargetCellValid(cell);
        
        // Set target map / position
        if (validTarget)
            TargetCell = cell;

        return validTarget;
    }

    /// <summary>
    /// Removes the action's current target.
    /// </summary>
    public void DeselectTarget()
    {
        TargetCell = null;
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
    public virtual IEnumerable<Cell> GetTargetableCells()
    {
        IEnumerable<Cell> cells = targetableStrategy.GetTargetableCells();

        // Return all cells that meet restrictions
        if (targetRestrictions != null && targetRestrictions.Count > 0)
        {
            foreach (Cell cell in cells)
            {
                if (targetRestrictions.All(r => r.IsTargetValid(cell)))
                    yield return cell;
            }
        }

        // Return all cells - no restrictions
        else
        {
            foreach (Cell cell in cells)
                yield return cell;
        }
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
    public IEnumerable<Cell> GetAffectedCoordinates()
    {
        return targetedStrategy.GetAffectedCoordinates();
    }

    /// <summary>
    /// Performs this action. Returns true if the action was
    /// successful.
    /// </summary>
    public virtual IEnumerator Do()
    {
        foreach (Cell cell in GetAffectedCoordinates())
        {
            // Apply action sequence to each affected cell.
            foreach (ActionNode action in actionSequence)
            {
                bool success = action.ApplyToCell(cell);
                if (!success)
                    break;
            }
        }

        return null;
    }

    /// <summary>
    /// Checks whether the given formation is a valid target.
    /// </summary>
    private bool IsTargetFormationValid(Formation formation)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (targetableFormation == TargetableFormation.All) return true;
        if (targetableFormation == TargetableFormation.None) return false;

        // Assume the formation is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        bool isSelfFormation = formation == Actor.Formation;

        // Can target own formation.
        if (targetableFormation.HasFlag(TargetableFormation.Self) &&
            isSelfFormation)
            valid = true;

        // Can target other formations.
        else if (targetableFormation.HasFlag(TargetableFormation.Other) &&
            !isSelfFormation)
            valid = true;

        return valid;
    }

    /// <summary>
    /// Checks whether the given cell is a valid target.
    /// </summary>
    private bool IsTargetCellValid(Cell tagetCell)
    {
        return GetTargetableCells().Any(cell => cell == tagetCell);
    }
}
