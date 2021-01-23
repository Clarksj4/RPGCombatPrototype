
public class HealNode : ActionNode
{
    public int Amount { get; set; }

    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
            pawn.GainHealth(Amount);

        return true;
    }
}
