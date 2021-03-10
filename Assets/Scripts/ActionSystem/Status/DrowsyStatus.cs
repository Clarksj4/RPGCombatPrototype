using UnityEngine;
using System;

[Serializable]
public class DrowsyStatus : PawnStatus
{
    [Tooltip("The duration that the target will sleep for after drowzy wears off.")]
    public int SleepDuration;

    public override PawnStatus Duplicate()
    {
        DrowsyStatus duplicate = base.Duplicate() as DrowsyStatus;
        duplicate.SleepDuration = SleepDuration;
        return duplicate;
    }

    protected override void OnExpired()
    {
        base.OnExpired();

        // Put pawn to sleep.
        Pawn.Statuses.Add(new SleepStatus() { Duration = SleepDuration });
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
