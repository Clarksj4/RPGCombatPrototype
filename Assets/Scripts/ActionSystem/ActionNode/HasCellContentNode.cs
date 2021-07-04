using SimpleBehaviourTree;
using UnityEngine;

public class HasCellContentNode : ActionNode
{
    [Tooltip("The content that is valid for the targeted cell.")]
    public TargetableCellContent Content = TargetableCellContent.All;

    public override bool Do(Blackboard state)
    {
        Pawn actor = state.Get<Pawn>("Actor");
        Cell target = state.Get<Cell>("Cell");

        CellContentRestriction restriction = new CellContentRestriction() { Content = Content };
        return restriction.IsTargetValid(actor, target);
    }
}
