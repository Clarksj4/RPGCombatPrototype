
public class ApplyStatusNode : ActionNode
{
    public PawnStatus Status { get; set; }

    public override bool Do(Pawn actor, Cell target)
    {
        Pawn pawn = target.GetContent<Pawn>();
        if (pawn != null)
            pawn.AddStatus(Status);
        return true;
    }
}
