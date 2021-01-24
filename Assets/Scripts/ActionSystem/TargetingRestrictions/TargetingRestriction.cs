
public abstract class TargetingRestriction
{
    /// <summary>
    /// Gets or sets the actor who is targeting
    /// the cells.
    /// </summary>
    public Pawn Actor { get; set; }

    public abstract bool IsTargetValid(Cell cell); 
}
