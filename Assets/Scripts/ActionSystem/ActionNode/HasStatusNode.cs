
public class HasStatusNode<T> : ActionNode where T : PawnStatus
{
    public override bool Do()
    {
        Pawn defender = Target.GetContent<Pawn>();
        if (defender != null)
            return defender.HasStatus<T>();

        // Fudge it if there is no defender
        return true;
    }
}
