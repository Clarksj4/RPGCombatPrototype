using SimpleBehaviourTree;
using UnityEngine;

public class HealNode : ActionNode
{
    [Tooltip("The amount to heal the target.")]
    public int Amount;

    public override bool Do(Blackboard state)
    {
        Pawn pawn = state.Get<Cell>("Cell")
                        ?.GetContent<Pawn>();
        if (pawn != null)
        {
            // Heal pawn
            pawn.Stats["Health"].Increment(Amount);

            // Update state with heal amount
            state["Health"] = state.Get<int>("Health") + Amount;

            return true;
        }

        // Nothing was healed.
        return false;
    }
}
