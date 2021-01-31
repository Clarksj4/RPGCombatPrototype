
public class HasCellContentNode : ActionNode
{
    /// <summary>
    /// Gets or sets the content that is valid for the
    /// targeted cell.
    /// </summary>
    public TargetableCellContent Content { get; set; }

    public override bool Do(Pawn actor, Cell target)
    {
        CellContentRestriction restriction = new CellContentRestriction() { Content = Content };
        return restriction.IsTargetValid(actor, target);
    }
}
