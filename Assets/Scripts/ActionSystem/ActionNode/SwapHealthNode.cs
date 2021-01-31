
public class SwapHealthNode : ActionNode
{
    public override bool Do(Pawn actor, Cell target)
    {
        Pawn pawn = target.GetContent<Pawn>();
        if (pawn != null)
        {
            int originHealth = actor.Health;
            int targetHealth = pawn.Health;

            pawn.SetHealth(originHealth);
            actor.SetHealth(targetHealth);
        }
            
        return true;
    }
}
