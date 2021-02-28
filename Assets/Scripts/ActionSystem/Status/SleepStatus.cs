using UnityEngine;
using System;

[Serializable]
public class SleepStatus : PawnStatus
{
    protected override void OnApplication()
    {
        Pawn.Stats["Sleeping"].Value += 1;
        Pawn.Stats["Health"].OnValueChanged += Pawn_OnHealthChanged;
    }

    protected override void OnExpired()
    {
        Pawn.Stats["Health"].OnValueChanged -= Pawn_OnHealthChanged;
        Pawn.Stats["Sleeping"].Value -= 1;
    }

    private void Pawn_OnHealthChanged(Stat stat, int delta)
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
