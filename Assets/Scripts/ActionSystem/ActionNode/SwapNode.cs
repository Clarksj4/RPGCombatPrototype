using UnityEngine;
using System.Collections;

public class SwapNode : ActionNode
{
    public SwapNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
            action.Actor.Swap(pawn);

        return true;
    }
}
