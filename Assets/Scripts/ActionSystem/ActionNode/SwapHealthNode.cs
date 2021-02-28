
public class SwapHealthNode : ActionNode
{
    public override bool Do(Pawn actor, Cell target)
    {
        Pawn pawn = target.GetContent<Pawn>();
        if (pawn != null)
        {
            int originHealth = actor.Stats["Health"].Value;
            int targetHealth = pawn.Stats["Health"].Value;

            pawn.Stats["Health"].Value = originHealth;
            actor.Stats["Health"].Value = targetHealth;
        }
            
        return true;
    }
}
