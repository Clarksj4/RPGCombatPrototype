
public abstract class ActionNode
{
    /// <summary>
    /// Gets or sets the cell that this action will target.
    /// </summary>
    public Cell Target { get; set; }
    /// <summary>
    /// Gets or sets the actor performing this action.
    /// </summary>
    public Pawn Actor { get; set; }

    public abstract bool Do();
}
