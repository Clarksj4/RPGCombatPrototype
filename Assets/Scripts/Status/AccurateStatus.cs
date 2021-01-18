using UnityEngine;
using System.Collections;

public class AccurateStatus : PawnStatus
{
    private int bonusAccuracy;
    public AccurateStatus(Pawn pawn, int duration, int bonusAccuracy)
        : base(pawn, duration)
    {
        this.bonusAccuracy = bonusAccuracy;
    }

    protected override void OnApplication()
    {
        base.OnApplication();
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Accuracy += bonusAccuracy;
        }

        else
            Expire();
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Actor actor = Pawn as Actor;

        actor.Accuracy = Mathf.Max(0, actor.Accuracy - bonusAccuracy);
    }
}
