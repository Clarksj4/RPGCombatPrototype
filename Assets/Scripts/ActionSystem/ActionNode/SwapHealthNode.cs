using UnityEngine;
using System.Collections;

public class SwapHealthNode : ActionNode
{
    public SwapHealthNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
        {
            int originHealth = action.Actor.Health;
            int targetHealth = pawn.Health;

            pawn.SetHealth(originHealth);
            action.Actor.SetHealth(targetHealth);
        }
            
        return true;
    }
}
