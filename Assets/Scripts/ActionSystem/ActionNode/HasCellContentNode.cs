
using SimpleBehaviourTree;

public class HasCellContentNode : ActionNode
{
    /// <summary>
    /// Gets or sets the content that is valid for the
    /// targeted cell.
    /// </summary>
    public TargetableCellContent Content = TargetableCellContent.All;

    public override bool Do(BehaviourTreeState state)
    {
        Pawn actor = state.Get<Pawn>("Actor");
        Cell target = state.Get<Cell>("Cell");

        CellContentRestriction restriction = new CellContentRestriction() { Content = Content };
        return restriction.IsTargetValid(actor, target);
    }
}
