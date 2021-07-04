using SimpleBehaviourTree;
using UnityEngine;

public class RemoveManaNode : ActionNode
{
    [Tooltip("The amount of mana to remove from the target.")]
    public int Amount;

    public override bool Do(Blackboard state)
    {
        Pawn target = state.Get<Cell>("Cell")
                          ?.GetContent<Pawn>();

        // Remove mana if there is a target.
        bool isTarget = target != null;
        if (isTarget)
            target.Stats["Mana"]?.Decrement(Amount);

        // Success if there was a target to remove mana from.
        return isTarget;
    }
}
