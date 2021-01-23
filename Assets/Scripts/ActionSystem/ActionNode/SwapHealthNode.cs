
public class SwapHealthNode : ActionNode
{
    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
        {
            int originHealth = Actor.Health;
            int targetHealth = pawn.Health;

            pawn.SetHealth(originHealth);
            Actor.SetHealth(targetHealth);
        }
            
        return true;
    }
}
