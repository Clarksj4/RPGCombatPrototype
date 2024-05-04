
public class ManaRestriction : TargetingRestriction, IQuantifyable
{
    /// <summary>
    /// Gets or sets the amount of mana the target
    /// must have to be valid.
    /// </summary>
    public int Amount;

    int IQuantifyable.Amount => Amount;

    public override bool IsTargetValid(Pawn actor, Cell cell)
    {
        Pawn pawn = cell.GetContent<Pawn>();
        if (pawn != null)
            return pawn.Stats["Mana"].Value >= Amount;

        return true;
    }
}
