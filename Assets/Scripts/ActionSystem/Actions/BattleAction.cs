using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    public Actor Actor { get; private set; }
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
    /// The collection of restrictions on the state of the actor.
    /// using this ability.
    /// </summary>
    protected List<TargetingRestriction> actorRestrictions = null;
    /// <summary>
    /// The collection of restrictions on cells that can be targeted.
    /// </summary>
    protected List<TargetingRestriction> targetRestrictions = null;
    /// <summary>
    /// The collection of areas that this action with affect.
    /// </summary>
    protected List<AffectedArea> areaOfEffect = null;
    /// <summary>
    /// The collection of restrictions on what cells this action will affect.
    /// </summary>
    protected List<TargetingRestriction> areaOfEffectRestrictions = null;

    //
    // Performed actions.
    //

    /// <summary>
    /// The sequence of things this battle action will do first.
    /// </summary>
    protected List<ActionNode> beginningActions = null;
    /// <summary>
    /// The sequence of things this battle action with do to 
    /// each of the targeted cells.
    /// </summary>
    protected List<ActionNode> targetedActions = null;
    /// <summary>
    /// The sequence of things this battle action will do last.
    /// </summary>
    protected List<ActionNode> endActions = null;

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
    /// Sets the actor that this action originates from.
    /// </summary>
    public void SetActor(Actor actor)
    {
        Actor = actor;
        Setup();
        targetableCells = GetTargetableCells().ToList();
    }

    /// <summary>
    /// Setup the action internal state once the actor is konwn.
    /// </summary>
    protected abstract void Setup();

    /// <summary>
    /// Sets the target for this action if valid. Returns true
    /// if the target is a valid one.
    /// </summary>
    public bool SetTarget(Cell cell)
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
    private IEnumerable<Cell> GetTargetableCells()
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
        IEnumerable<Cell> area = areaOfEffect.SelectMany(a => a.GetAffectedArea());
        
        // If there are further restrictions, only return cells that conform
        if (areaOfEffectRestrictions != null &&
            areaOfEffectRestrictions.Count > 0)
            return area.Where(cell => areaOfEffectRestrictions.All(restriction => restriction.IsTargetValid(cell)));
     
        // Otherwise, return the area as it is.
        else
            return area;
    }

    /// <summary>
    /// Gets whether the assigned actor is able to complete
    /// the action with its current parameters.
    /// </summary>
    public bool CanDo()
    {
        // No restreictions OR conforms to all restrictions.
        return actorRestrictions == null || 
               actorRestrictions.Count == 0 ||
               actorRestrictions.All(r => r.IsTargetValid(OriginCell));
    }

    /// <summary>
    /// Performs this action. Returns true if the action was
    /// successful.
    /// </summary>
    public virtual IEnumerator Do()
    {
        bool success = DoBeginningActions();

        if (success)
            DoTargetedActions();

        DoEndActions();
        
        return null;
    }

    private bool DoBeginningActions()
    {
        // Don't do anything if there's nothing to do.
        if (beginningActions != null &&
            beginningActions.Count > 0)
        {
            foreach (ActionNode node in beginningActions)
            {
                if (!node.Do())
                    return false;
            }
        }

        // Also assumes success if there was nothing to do.
        return true;
    }

    private void DoTargetedActions()
    {
        if (targetedActions != null &&
            targetedActions.Count > 0)
        {
            bool success;
            foreach (Cell cell in affectedCells)
            {
                foreach (ActionNode node in targetedActions)
                {
                    node.Target = cell;
                    success = node.Do();

                    // If one of the nodes fails - abort subsequent 
                    // node execution
                    if (!success)
                        break;
                }

                // If we don't mind about failures between cells
                // then reset the success state
                if (AffectedCellsIndependent)
                    success = true;

                // Otherwise, end iteration.
                else break;
            }
        }
    }

    private void DoEndActions()
    {
        if (endActions != null &&
            endActions.Count > 0)
        {
            foreach (ActionNode node in endActions)
            {
                if (!node.Do())
                    break;
            }
        }
    }

    /// <summary>
    /// Checks whether the given cell is a valid target.
    /// </summary>
    private bool IsTargetCellValid(Cell tagetCell)
    {
        return targetableCells.Any(cell => cell == tagetCell);
    }
}
