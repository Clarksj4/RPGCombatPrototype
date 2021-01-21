using UnityEngine;
using System.Collections;

public class SetHealthNode : ActionNode
{
    private int amount;

    public SetHealthNode(BattleAction action, int amount)
        : base(action) 
    {
        this.amount = amount;
    }

    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
            pawn.SetHealth(amount);
        return true;
    }
}
