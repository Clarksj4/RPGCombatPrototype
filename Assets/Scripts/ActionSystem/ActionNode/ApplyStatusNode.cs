
public class ApplyStatusNode : ActionNode
{
    public PawnStatus Status { get; set; }

    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
            pawn.AddStatus(Status);
        return true;
    }
}
