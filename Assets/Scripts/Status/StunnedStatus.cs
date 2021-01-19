using UnityEngine;

public class StunnedStatus : PawnStatus
{
    protected override void OnApplication()
    {
        base.OnApplication();

        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Incapacitated = true;
        }

        else
            Expire();
    }

    protected override void OnExpired()
    {
        base.OnExpired();

        Actor actor = Pawn as Actor;
        actor.Incapacitated = false;
    }

    public override bool Collate(PawnStatus other)
    {
        if (other is StunnedStatus)
        {
            Duration = Mathf.Max(Duration, other.Duration);
            return true;
        }

        return false;
    }
}
