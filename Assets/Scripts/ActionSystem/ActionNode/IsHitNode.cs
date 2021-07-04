using SimpleBehaviourTree;

public class IsHitNode : ActionNode
{
    public override bool Do(Blackboard state)
    {
        Pawn defender = state.Get<Cell>("Cell")
                            ?.GetContent<Pawn>();
        if (defender != null)
            return defender.IsHit();

        // Nothing to hit.
        return false;
    }
}
