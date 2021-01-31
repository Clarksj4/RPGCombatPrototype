
public class HealNode : ActionNode
{
    public int Amount { get; set; }

    public override bool Do(Pawn actor, Cell target)
    {
        Pawn pawn = target.GetContent<Pawn>();
        if (pawn != null)
            pawn.GainHealth(Amount);

        return true;
    }
}
