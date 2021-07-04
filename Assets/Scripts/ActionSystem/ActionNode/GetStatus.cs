using UnityEngine;
using System.Collections;
using SimpleBehaviourTree;
using System.Collections.Generic;
using System.Linq;

public class GetStatus : ActionNode
{
    public override bool Do(Blackboard state)
    {
        Pawn target = state.Get<Cell>("Cell")
                          ?.GetContent<Pawn>();
        
        if (target != null)
        {
            foreach (PawnStatus status in target.Statuses)
                state.Add(status.Name, status);

            return target.Statuses.Any();
        }

        return false;
    }
}
