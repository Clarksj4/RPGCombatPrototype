
public class SetHealthNode : ActionNode
{
    public int Amount;

    public override bool Do(Pawn actor, Cell target)
    {
        Pawn pawn = target.GetContent<Pawn>();
        if (pawn != null)
            pawn.Stats["Health"].Value = Amount;
        return true;
    }
}
