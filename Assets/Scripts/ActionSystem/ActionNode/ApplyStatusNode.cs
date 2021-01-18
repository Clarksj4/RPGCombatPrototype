using UnityEngine;
using System.Collections;

public class ApplyStatusNode : ActionNode
{
    public PawnStatus Status { get; set; }

    public ApplyStatusNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
            pawn.AddStatus(Status);
        return true;
    }
}
