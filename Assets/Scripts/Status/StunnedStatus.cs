using UnityEngine;

public class StunnedStatus : PawnStatus
{
    public StunnedStatus(int duration)
        : base(duration) { /* Nothing! */ }

    protected override void OnApplication()
    {
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Stunned = true;
        }

        else
            Expire();
    }

    protected override void OnExpired()
    {
        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Stunned = false;
        }
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
