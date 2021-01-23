using UnityEngine;
using System;

[Serializable]
public class PowerStatus : PawnStatus
{
    public PowerStatus(int duration)
        : base(duration) { /* Nothing! */ }

    protected override void OnApplication()
    {
        base.OnApplication();
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Power += 0.2f;
        }
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Actor actor = Pawn as Actor;
        actor.Power = Mathf.Max(0, actor.Power - 0.2f);
    }
}
