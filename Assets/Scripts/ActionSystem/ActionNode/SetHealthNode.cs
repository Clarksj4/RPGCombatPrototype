
using SimpleBehaviourTree;
using UnityEngine;

public class SetHealthNode : ActionNode
{
    [Tooltip("The value to set the target's health to.")]
    public int Amount;

    public override bool Do(Blackboard state)
    {
        Pawn target = state.Get<Cell>("Cell")
                          ?.GetContent<Pawn>();

        // Remove mana if there is a target to remove mana from.
        bool isTarget = target != null;
        if (isTarget)
            target.Stats["Health"].Value = Amount;

        // Success if there was a target to remove mana from.
        return isTarget;
    }
}
