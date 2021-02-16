using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Encapsulates any action / ability used by an pawn
/// during a battle.
/// </summary>
[CreateAssetMenu(fileName = "New Action", menuName = "Battle Action")]
public class BattleAction : SerializedScriptableObject
{
    [TitleGroup("Battle Action")]
    [HorizontalGroup("Battle Action/Misc")]
    [VerticalGroup("Battle Action/Misc/Left")]
    [Tooltip("The maximum number of uses of this action per turn.")]
    public int MaxUsesPerTurn = 1;

    [VerticalGroup("Battle Action/Misc/Left")]
    [Tooltip("The minimum number of turns between uses of this action.")]
    public int Cooldown = 1;

    [VerticalGroup("Battle Action/Misc/Left")]
    [Tooltip("If true, an action node failure will only prevent subsequent action on the cell where the failure occurred.")]
    public bool AffectedCellsIndependent = true;

    [VerticalGroup("Battle Action/Misc/Right")]
    [HideLabel, PreviewField(Height = 100, Alignment = ObjectFieldAlignment.Right)]
    public Sprite Sprite;

    [BoxGroup("Prerequisites", CenterLabel = true)]
    [OdinSerialize]
    [Tooltip("The actor must conform to these restrictions in order to use this action.")]
    protected List<TargetingRestriction> actorRestrictions = new List<TargetingRestriction>(0);

    [BoxGroup("Targetable Cells", CenterLabel = true)]
    [OdinSerialize]
    [Tooltip("Cells must conform to these restrictions in order to be targeted.")]
    protected List<TargetingRestriction> targetRestrictions = new List<TargetingRestriction>(0);

    [BoxGroup("Area of Effect", CenterLabel = true)]
    [OdinSerialize]
    [Tooltip("The area that this battle action will affect - originates from the targeted cell.")]
    protected List<AffectedArea> areaOfEffect = new List<AffectedArea>(0);

    [BoxGroup("Area of Effect")]
    [OdinSerialize]
    [Tooltip("Targeted cells must conform to these restrictions in order to be affected.")]
    protected List<TargetingRestriction> areaOfEffectRestrictions = new List<TargetingRestriction>(0);

    [BoxGroup("Effect on Caster", CenterLabel = true)]
    [OdinSerialize]
    [Tooltip("The sequence of things this battle action will do on the caster.")]
    protected List<ActionNode> selfActions = new List<ActionNode>(0);

    [BoxGroup("Effect on Targeted Cells", CenterLabel = true)]
    [OdinSerialize]
    [Tooltip("The sequence of things this battle action will do on each targeted cell.")]
    protected List<ActionNode> targetedActions = new List<ActionNode>(0);

    //
    // Properties
    //

    /// <summary>
    /// Gets a collection of informative tags about this action.
    /// </summary>
    public ActionTag Tags { get; protected set; } = ActionTag.None;
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
    /// <summary>
    /// The collection of restrictions on cells that can be targeted.
    /// </summary>
    public IEnumerable<TargetingRestriction> ActorRestrictions { get { return actorRestrictions; } }
    /// <summary>
    /// The collection of restrictions on cells that can be targeted.
    /// </summary>
    public IEnumerable<TargetingRestriction> TargetingRestrictions { get { return targetRestrictions; } }
    /// <summary>
    /// The sequence of things this battle action will do first.
    /// </summary>
    public IEnumerable<ActionNode> SelfActions { get { return selfActions; } }
    /// <summary>
    /// The sequence of things this battle action with do to 
    /// each of the targeted cells.
    /// </summary>
    public IEnumerable<ActionNode> TargetedActions { get { return targetedActions; } }
    /// <summary>
    /// Gets all the cells targetable by this action.
    /// </summary>
    public IEnumerable<Cell> TargetableCells { get { return targetableCells; } }
    /// <summary>
    /// Gets all the cells affected by this action.
    /// </summary>
    public IEnumerable<Cell> AffectedCells { get { return affectedCells; } }

    //
    // Fields
    //

    private IList<Cell> targetableCells = null;
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
        targetableCells = GetTargetableCells().ToList();
    }

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
    /// Removes this action's current actor.
    /// </summary>
    public void DeselectActor()
    {
        Actor = null;
        targetableCells = null;
    }

    /// <summary>
    /// Removes this action's current target.
    /// </summary>
    public void DeselectTarget()
    {
        TargetCell = null;
        affectedCells = null;
    }

    /// <summary>
    /// Gets whether the assigned actor is able to complete
    /// the action with its current parameters.
    /// </summary>
    public bool CanDo()
    {
        // No restreictions OR conforms to all restrictions.
        return Actor.GetActionUseCount(name) < MaxUsesPerTurn &&
              (actorRestrictions == null ||
               actorRestrictions.Count == 0 ||
               actorRestrictions.All(r => r.IsTargetValid(Actor, OriginCell)));
    }

    /// <summary>
    /// Performs this action. Returns true if the action was
    /// successful.
    /// </summary>
    public void Do()
    {
        // Do self targeted actions FIRST!
        bool success = DoSelfActions();

        // Do targeted actions if the self actions were
        // successful
        if (success)
            DoTargetedActions();
    }

    /// <summary>
    /// Gets all the cells that can be targeted by this action.
    /// </summary>
    private IEnumerable<Cell> GetTargetableCells()
    {
        // If there are restrictions then only return cells
        // that conform.
        if (targetRestrictions != null && targetRestrictions.Count > 0)
        {
            // Iterate through ALL cells.
            foreach (Cell cell in Grid.GetCells())
            {
                // Check if EACH cell conforms to ALL
                // restrictions.
                if (targetRestrictions.All(r => r.IsTargetValid(Actor, cell)))
                    yield return cell;
            }
        }

        // There are NO restrictions - return all cells.
        else
        {
            foreach (Cell cell in Grid.GetCells())
                yield return cell;
        }
    }

    /// <summary>
    /// Checks whether the given cell is a valid target.
    /// </summary>
    private bool IsTargetCellValid(Cell tagetCell)
    {
        return targetableCells.Any(cell => cell == tagetCell);
    }

    /// <summary>
    /// Gets the coordinates that will be affected by this action.
    /// </summary>
    private IEnumerable<Cell> GetAffectedCells()
    {
        // Get cells defined by area.
        IEnumerable<Cell> area = areaOfEffect.SelectMany(a => a.GetAffectedArea(TargetCell));
        
        // Get rid of cells in area that don't conform to these
        // additional restrictions - for example, some actions
        // might require cells in the area to be empty.
        if (areaOfEffectRestrictions != null &&
            areaOfEffectRestrictions.Count > 0)
            return area.Where(cell => areaOfEffectRestrictions.All(restriction => restriction.IsTargetValid(Actor, cell)));
     
        // Otherwise, return the area as it is.
        else
            return area;
    }

    private bool DoSelfActions()
    {
        // Don't do anything if there's nothing to do.
        if (selfActions != null &&
            selfActions.Count > 0)
        {
            foreach (ActionNode node in selfActions)
            {
                // If the node fails - abort iteration.
                if (!node.Do(Actor, OriginCell))
                    return false;
            }
        }

        // Also assumes success if there was nothing to do.
        return true;
    }

    private void DoTargetedActions()
    {
        // Only do things if there ARE targeted actions to perform.
        if (targetedActions != null &&
            targetedActions.Count > 0)
        {
            // Apply actions to each affected cell.
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
}
