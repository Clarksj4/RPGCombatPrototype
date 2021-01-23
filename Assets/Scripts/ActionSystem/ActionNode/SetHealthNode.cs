
public class SetHealthNode : ActionNode
{
    public int Amount { get; set; }

    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
            pawn.SetHealth(Amount);
        return true;
    }
}
