using UnityEngine;
using System.Collections;

public class WeakenedStatus : PawnStatus
{
    public WeakenedStatus(int duration)
        : base(duration) { /* Nothing! */ }

    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Power -= 0.5f;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Power += 0.5f;
    }

    public override bool Collate(PawnStatus other)
    {
        if (other is WeakenedStatus)
        {
            Duration += other.Duration;
            return false;
        }

        return true;
    }

}
