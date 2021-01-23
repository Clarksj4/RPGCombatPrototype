using System;
using UnityEngine;

[Serializable]
public class StunnedStatus : PawnStatus
{
    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Stunned = true;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Stunned = false;
    }

    public override bool Collate(PawnStatus other)
    {
        // Can stack stuns - just pick the longest duration
        if (other is StunnedStatus)
        {
            Duration = Mathf.Max(Duration, other.Duration);
            return true;
        }
       
        // Can't be put to sleep or made drowsy while stunned.
        else if (other is SleepStatus ||
                 other is DrowsyStatus)
            return true;

        return false;
    }
}
