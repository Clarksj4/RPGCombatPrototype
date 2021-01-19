using UnityEngine;
using System.Collections;

public class ImmobilizedStatus : PawnStatus
{
    private int removedMovement = 0;

    protected override void OnApplication()
    {
        base.OnApplication();
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            removedMovement = actor.Movement;
            actor.Movement = 0;
        }

        else
            Expire();
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Actor actor = Pawn as Actor;
        actor.Movement += removedMovement;
    }

    public override bool Collate(PawnStatus other)
    {
        // Block agility status
        return other is AgilityStatus;
    }
}
