using UnityEngine;
using System.Collections;

public class PowerStatus : PawnStatus
{
    public float BonusPower { get; set; }

    protected override void OnApplication()
    {
        base.OnApplication();
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Power += BonusPower;
        }
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Actor actor = Pawn as Actor;
        actor.Power = Mathf.Max(0, actor.Power - BonusPower);
    }
}
