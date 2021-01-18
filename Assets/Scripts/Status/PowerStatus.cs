using UnityEngine;
using System.Collections;

public class PowerStatus : PawnStatus
{
    private int bonusAttack;
    public PowerStatus(Pawn pawn, int duration, int bonusAttack)
        : base(pawn, duration)
    {
        this.bonusAttack = bonusAttack;
    }

    protected override void OnApplication()
    {
        base.OnApplication();
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Attack += bonusAttack;
        }
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Actor actor = Pawn as Actor;
        actor.Attack = Mathf.Max(0, actor.Attack - bonusAttack);
    }
}
