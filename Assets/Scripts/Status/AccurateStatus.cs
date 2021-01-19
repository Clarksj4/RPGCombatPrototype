using UnityEngine;
using System.Collections;

public class AccurateStatus : PawnStatus
{
    public int BonusAccuracy { get; set; }

    protected override void OnApplication()
    {
        base.OnApplication();
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Accuracy += BonusAccuracy;
        }

        else
            Expire();
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Actor actor = Pawn as Actor;

        actor.Accuracy = Mathf.Max(0, actor.Accuracy - BonusAccuracy);
    }
}
