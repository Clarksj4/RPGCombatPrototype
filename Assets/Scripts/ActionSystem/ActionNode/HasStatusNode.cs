using SimpleBehaviourTree;
using UnityEngine;

public class HasStatusNode : ActionNode
{
    [Tooltip("The status to apply to the target.")]
    public string StatusName;

    public override bool Do(BehaviourTreeState state)
    {
        Pawn defender = state.Get<Cell>("Cell")
                            ?.GetContent<Pawn>();
        if (defender != null)
            return defender.Statuses.Contains(StatusName);

        // Doesn't have status
        return false;
    }
}
