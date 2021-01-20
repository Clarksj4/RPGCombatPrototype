using System.Linq;
using UnityEngine;

public class DoDamageNode : ActionNode
{
    private int baseDamage;
    private bool amplifyable;
    private bool defendable;

    public DoDamageNode(BattleAction action, int baseDamage, bool amplifyable = true, bool defendable = true)
        : base(action) 
    {
        this.baseDamage = baseDamage;
        this.amplifyable = amplifyable;
        this.defendable = defendable;
    }

    public override bool Do()
    {
        Pawn defender = Target.GetContent<Pawn>();
        if (defender != null)
        {
            int inflicted = amplifyable ? (int)(baseDamage * action.Actor.Power) : baseDamage;
            defender.TakeDamage(inflicted, defendable);
        }
        
        return true;
    }
}
