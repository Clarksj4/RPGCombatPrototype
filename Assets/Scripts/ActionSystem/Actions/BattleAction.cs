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
    /// Gets the collection of restrictions on cells that can be targeted.
    /// </summary>
    protected List<TargetingRestriction> targetRestrictions = null;
    /// <summary>
    /// Gets a list of areas that this action will affect.
    /// </summary>
    protected List<AffectedArea> areaOfEffect = null;
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
    /// Gets a collection of informative tags about this action.
    /// </summary>
    public ActionTag Tags { get; protected set; } = ActionTag.None;
    /// <summary>
    /// Gets or sets how action node failures are handled.
    /// </summary>
    public bool AffectedCellsIndependent { get; set; } = true;

    //
    // Targetable cells cache
    //

    /// <summary>
    /// Gets all the cells targetable by this action.
    /// </summary>
    public IEnumerable<Cell> TargetableCells { get { return targetableCells; } }
    private IList<Cell> targetableCells = null;

    //
    // Affected cells cache
    //

    /// <summary>
    /// Gets all the cells affected by this action.
    /// </summary>
    public IEnumerable<Cell> AffectedCells { get { return affectedCells; } }
    private IList<Cell> affectedCells = null;

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
            targetableCells = GetTargetableCells().ToList();
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
        {
            TargetCell = cell;
            affectedCells = GetAffectedCells().ToList();
        }

        return validTarget;
    }

    /// <summary>
    /// Removes the action's current target.
    /// </summary>
    public void DeselectTarget()
    {
        TargetCell = null;
        affectedCells = null;
    }

    /// <summary>
    /// Gets all the cells that can be targeted by this action.
    /// </summary>
    protected virtual IEnumerable<Cell> GetTargetableCells()
    {
        // Return all cells that meet restrictions
        if (targetRestrictions != null && targetRestrictions.Count > 0)
        {
            foreach (Cell cell in Grid.GetCells())
            {
                if (targetRestrictions.All(r => r.IsTargetValid(cell)))
                    yield return cell;
            }
        }

        // Return all cells - no restrictions
        else
        {
            foreach (Cell cell in Grid.GetCells())
                yield return cell;
        }
    }

    /// <summary>
    /// Gets the coordinates that will be affected by this action.
    /// </summary>
    protected IEnumerable<Cell> GetAffectedCells()
    {
        return areaOfEffect.SelectMany(a => a.GetAffectedArea());
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
    /// Performs this action. Returns true if the action was
    /// successful.
    /// </summary>
    public virtual IEnumerator Do()
    {
        foreach (Cell cell in affectedCells)
        {
            // Apply action sequence to each affected cell.
            foreach (ActionNode action in actionSequence)
            {
                bool success = action.ApplyToCell(cell);
                if (!success)
                {
                    // If cells are affected independent of one another,
                    // continue affecting the remaining cells.
                    if (AffectedCellsIndependent)
                        break;

                    // If cells are not independent then abort the remainder
                    // of the action.
                    else
                        return null;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Checks whether the given cell is a valid target.
    /// </summary>
    private bool IsTargetCellValid(Cell tagetCell)
    {
        return targetableCells.Any(cell => cell == tagetCell);
    }
}
