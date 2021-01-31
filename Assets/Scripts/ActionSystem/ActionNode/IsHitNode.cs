
public class IsHitNode : ActionNode
{
    public override bool Do(Pawn actor, Cell target)
    {
        Pawn defender = target.GetContent<Pawn>();
        if (defender != null)
            return defender.IsHit();

        // Fudge it if there is no defender
        return true;
    }
}
