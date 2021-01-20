using System.Linq;
using UnityEngine;

public class IsHitNode : ActionNode
{
    public IsHitNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool Do()
    {
        Pawn defender = Target.GetContent<Pawn>();
        if (defender != null)
            return defender.IsHit();

        // Fudge it if there is no defender
        return true;
    }
}
