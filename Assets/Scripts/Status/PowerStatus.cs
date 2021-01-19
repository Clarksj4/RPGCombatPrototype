using UnityEngine;
using System.Collections;

public class PowerStatus : PawnStatus
{
    public int BonusAttack { get; set; }

    protected override void OnApplication()
    {
        base.OnApplication();
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Attack += BonusAttack;
        }
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Actor actor = Pawn as Actor;
        actor.Attack = Mathf.Max(0, actor.Attack - BonusAttack);
    }
}
