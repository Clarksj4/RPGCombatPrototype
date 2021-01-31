
public class SwapNode : ActionNode
{
    public override bool Do(Pawn actor, Cell target)
    {
        Pawn pawn = target.GetContent<Pawn>();
        if (pawn != null)
            actor.Swap(pawn);

        return true;
    }
}
