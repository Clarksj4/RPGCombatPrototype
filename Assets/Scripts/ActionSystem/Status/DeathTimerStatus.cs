using UnityEngine;

public class DeathTimerStatus : PawnStatus
{
    public override bool Collate(PawnStatus other)
    {
        // Can't stack death timers, just use the lowest duration.
        if (other is DeathTimerStatus)
        {
            Duration = Mathf.Min(other.Duration, Duration);
            return true;
        }

        return false;
    }

    protected override void OnExpired()
    {
        base.OnExpired();

        // He dead.
        Object.Destroy(Pawn.gameObject);
    }
}
