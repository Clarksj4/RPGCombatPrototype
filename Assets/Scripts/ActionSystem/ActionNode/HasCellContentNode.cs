
public class HasCellContentNode : ActionNode
{
    /// <summary>
    /// Gets or sets the content that is valid for the
    /// targeted cell.
    /// </summary>
    public TargetableCellContent Content { get; set; }

    public override bool Do()
    {
        CellContentRestriction restriction = new CellContentRestriction() { Actor = Actor, Content = Content };
        return restriction.IsTargetValid(Target);
    }
}
