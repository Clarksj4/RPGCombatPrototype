
public class ApplyStatusNode : ActionNode
{
    public PawnStatus Status;

    public override bool Do(Pawn actor, Cell target)
    {
        Status.Applicator = actor;
        Pawn pawn = target.GetContent<Pawn>();
        if (pawn != null)
            pawn.Statuses.Add(Status);
        return true;
    }
}
