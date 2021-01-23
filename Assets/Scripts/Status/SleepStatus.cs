using UnityEngine;
using System;

[Serializable]
public class SleepStatus : PawnStatus
{
    public SleepStatus(int duration)
        : base(duration) { /* Nothing! */ }

    protected override void OnApplication()
    {
        Pawn.Sleeping = true;
        Pawn.OnHealthChanged += Pawn_OnHealthChanged;
    }

    protected override void OnExpired()
    {
        Pawn.OnHealthChanged -= Pawn_OnHealthChanged;
        Pawn.Sleeping = false;
    }

    private void Pawn_OnHealthChanged(int delta)
    {
        // If Pawn takes damage - wake up!
        if (delta < 0)
            Expire();
    }

    public override bool Collate(PawnStatus other)
    {
        // Can't stack sleep - just pick the longest duration.
        if (other is SleepStatus)
        {
            Duration = Mathf.Max(other.Duration, Duration);
            return true;
        }

        // Can't get drowsy while sleeping
        else if (other is DrowsyStatus)
            return true;

        return false;
    }
}
