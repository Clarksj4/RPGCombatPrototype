
public class ManaRestriction : TargetingRestriction
{
    /// <summary>
    /// Gets or sets the amount of mana the target
    /// must have to be valid.
    /// </summary>
    public int Amount { get; set; }

    public override bool IsTargetValid(Cell cell)
    {
        Pawn pawn = cell.GetContent<Pawn>();
        if (pawn != null)
            return pawn.Mana >= Amount;

        return true;
    }
}
