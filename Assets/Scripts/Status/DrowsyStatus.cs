using UnityEngine;
using System.Collections;

public class DrowsyStatus : PawnStatus
{
    public int SleepDuration { get; private set; }

    public DrowsyStatus(int duration, int sleepDuration)
        : base(duration)
    {
        SleepDuration = sleepDuration;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.AddStatus(new SleepStatus(SleepDuration));
    }

    public override bool Collate(PawnStatus other)
    {
        // Get outta town!
        if (other is DrowsyStatus)
            return false;

        return true;
    }
}
