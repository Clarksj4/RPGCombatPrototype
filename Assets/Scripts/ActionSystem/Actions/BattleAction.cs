using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Encapsulates any action / ability used by an actor
/// during a battle.
/// </summary>
[CreateAssetMenu(fileName = "New Action", menuName = "Battle Action")]
public class BattleAction : SerializedScriptableObject
{
    /// <summary>
    /// Gets the name of this battle action.
    /// </summary>
    public string Name { get { return string.IsNullOrEmpty(name) ? GetType().Name.Replace("Action", "") : name; } }

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
    public Pawn Actor { get; private set; }
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
    
    public IEnumerable<TargetingRestriction> ActorRestrictions { get { return actorRestrictions; } }
    [BoxGroup("Prerequisites", CenterLabel = true)]
    [OdinSerialize][Tooltip("The actor must conform to these restrictions in order to use this action.")]
    protected List<TargetingRestriction> actorRestrictions = new List<TargetingRestriction>(0);
    /// <summary>
    /// The collection of restrictions on cells that can be targeted.
    /// </summary>
    public IEnumerable<TargetingRestriction> TargetingRestrictions { get { return targetRestrictions; } }
    [BoxGroup("Targetable Cells", CenterLabel = true)]
    [OdinSerialize][Tooltip("Cells must conform to these restrictions in order to be targeted.")]
    protected List<TargetingRestriction> targetRestrictions = new List<TargetingRestriction>(0);
    
    [BoxGroup("Area of Effect", CenterLabel = true)]
    [OdinSerialize][Tooltip("The area that this battle action will affect - originates from the targeted cell.")]
    protected List<AffectedArea> areaOfEffect = new List<AffectedArea>(0);
    
    [BoxGroup("Area of Effect")]
    [OdinSerialize][Tooltip("Targeted cells must conform to these restrictions in order to be affected.")]
    protected List<TargetingRestriction> areaOfEffectRestrictions = new List<TargetingRestriction>(0);

    //
    // Performed actions.
    //

    /// <summary>
    /// The sequence of things this battle action will do first.
    /// </summary>
    public IEnumerable<ActionNode> SelfActions { get { return selfActions; } }
    [Header("Effect on Caster")]
    [OdinSerialize][Tooltip("The sequence of things this battle action will do on the caster.")]
    protected List<ActionNode> selfActions = new List<ActionNode>(0);
    /// <summary>
    /// The sequence of things this battle action with do to 
    /// each of the targeted cells.
    /// </summary>
    public IEnumerable<ActionNode> TargetedActions { get { return targetedActions; } }
    [Header("Effect on Targeted Cells")]
    [OdinSerialize][Tooltip("The sequence of things this battle action will do on each targeted cell.")]
    protected List<ActionNode> targetedActions = new List<ActionNode>(0);

    //
    // Action params
    //

    /// <summary>
    /// Gets or sets the maximum uses of this action per turn.
    /// </summary>
    [Header("Misc Params")]
    [Tooltip("The maximum number of uses of this action per turn.")]
    public int MaxUsesPerTurn = 1;
    /// <summary>
    /// Gets a collection of informative tags about this action.
    /// </summary>
    public ActionTag Tags { get; protected set; } = ActionTag.None;
    /// <summary>
    /// Gets or sets how action node failures are handled.
    /// </summary>
    [Tooltip("If true, an action node failure will only prevent subsequent action on the cell where the failure occurred.")]
    public bool AffectedCellsIndependent = true;

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
    public void SetActor(Pawn actor)
    {
        Actor = actor;
        Setup();
        targetableCells = GetTargetableCells().ToList();
    }

    /// <summary>
    /// Setup the action internal state once the actor is konwn.
    /// </summary>
    protected virtual void Setup() { /* Nothing! */ }

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
    /// Resets this actions actor and target.
    /// </summary>
    public void Clear()
    {
        Actor = null;
        TargetCell = null;

        targetableCells.Clear();
        affectedCells.Clear();
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
                if (targetRestrictions.All(r => r.IsTargetValid(Actor, cell)))
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
        IEnumerable<Cell> area = areaOfEffect.SelectMany(a => a.GetAffectedArea(TargetCell));
        
        // If there are further restrictions, only return cells that conform
        if (areaOfEffectRestrictions != null &&
            areaOfEffectRestrictions.Count > 0)
            return area.Where(cell => areaOfEffectRestrictions.All(restriction => restriction.IsTargetValid(Actor, cell)));
     
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
        return Actor.GetActionUseCount(Name) < MaxUsesPerTurn && 
              (actorRestrictions == null || 
               actorRestrictions.Count == 0 ||
               actorRestrictions.All(r => r.IsTargetValid(Actor, OriginCell)));
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
        
        return null;
    }

    private bool DoBeginningActions()
    {
        // Don't do anything if there's nothing to do.
        if (selfActions != null &&
            selfActions.Count > 0)
        {
            foreach (ActionNode node in selfActions)
            {
                if (!node.Do(Actor, OriginCell))
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
                    success = node.Do(Actor, cell);

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

    /// <summary>
    /// Checks whether the given cell is a valid target.
    /// </summary>
    private bool IsTargetCellValid(Cell tagetCell)
    {
        return targetableCells.Any(cell => cell == tagetCell);
    }
}
