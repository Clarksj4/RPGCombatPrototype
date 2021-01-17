using System.Linq;
using UnityEngine;

public class DoDamageNode : ActionNode
{
    public int Amount { get; set; }

    public DoDamageNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool Do()
    {
        Pawn defender = Target.GetContent<Pawn>();
        if (defender != null)
        {
            // If target is same as origin then lose health instead of taking attack
            if (defender == action.Actor)
                defender.LoseHealth(Amount);
            else
                defender.TakeAttack(action.Actor);
        }
            

        return true;
    }
}
