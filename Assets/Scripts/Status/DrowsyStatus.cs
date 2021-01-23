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
    public int SleepDuration { get { return sleepDuration; } }
    [SerializeField]
    private int sleepDuration;

    public DrowsyStatus(int duration, int sleepDuration)
        : base(duration)
    {
        this.sleepDuration = sleepDuration;
    }

    protected override void OnExpired()
    {
        base.OnExpired();

        // Put pawn to sleep.
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
