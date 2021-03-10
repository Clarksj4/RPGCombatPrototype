using SimpleBehaviourTree;

public class SwapHealthNode : ActionNode
{
    public override bool Do(BehaviourTreeState state)
    {
        Pawn actor = state.Get<Pawn>("Actor");
        Pawn target = state.Get<Cell>("Cell")
                          ?.GetContent<Pawn>();

        // Swap health if there's a target
        bool isTarget = target != null;
        if (isTarget)
        {
            int originHealth = actor.Stats["Health"].Value;
            int targetHealth = target.Stats["Health"].Value;

            target.Stats["Health"].Value = originHealth;
            actor.Stats["Health"].Value = targetHealth;
        }
            
        // Success if there was a target to swap with.
        return isTarget;
    }
}
