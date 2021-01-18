using UnityEngine;
using System.Collections;

public class AgilityStatus : PawnStatus
{
    private int bonusMovement;

    public AgilityStatus(Pawn pawn, int duration, int bonusMovement)
        : base(pawn, duration)
    {
        this.bonusMovement = bonusMovement;
    }

    protected override void OnApplication()
    {
        base.OnApplication();
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Movement += bonusMovement;
        }

        else
            Expire();
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Actor actor = Pawn as Actor;
        
        // Remove the movement again, but don't let movement fall below 0
        actor.Movement = Mathf.Max(0, actor.Movement - bonusMovement);
    }
}
