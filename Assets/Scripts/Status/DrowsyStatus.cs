using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class DrowsyStatus : PawnStatus
{
    /// <summary>
    /// Gets the number of turns that the targeted pawn will
    /// sleep for when drowsy expires.
    /// </summary>
    public int SleepDuration;

    protected override void OnExpired()
    {
        base.OnExpired();

        // Put pawn to sleep.
        Pawn.AddStatus(new SleepStatus() { Duration = SleepDuration });
    }

    public override bool Collate(PawnStatus other)
    {
        // Get outta town!
        if (other is DrowsyStatus)
            return true;

        // Sleep overrides drowsy - remove this status
        else if (other is SleepStatus)
            Expire();

        return false;
    }
}
