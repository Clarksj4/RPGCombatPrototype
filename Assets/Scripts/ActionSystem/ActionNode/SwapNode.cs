
public class SwapNode : ActionNode
{
    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
            Actor.Swap(pawn);

        return true;
    }
}
