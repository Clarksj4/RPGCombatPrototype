using SimpleBehaviourTree;

public class SwapNode : ActionNode
{
    public override bool Do(Blackboard state)
    {
        Pawn actor = state.Get<Pawn>("Actor");
        Pawn target = state.Get<Cell>("Cell")
                          ?.GetContent<Pawn>();

        bool isTarget = target != null;
        if (isTarget)
            actor.Swap(target);

        // Success if there was a target to swap with.
        return isTarget;
    }
}
