using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class HobbleStatus : PawnStatus
{
    /// <summary>
    /// Gets the number of turns that the targeted pawn will
    /// be immobilized for when hobble expires.
    /// </summary>
    public int ImmobilizeDuration;

    protected override void OnExpired()
    {
        base.OnExpired();

        // Immobilize pawn.
        Pawn.Statuses.Add(new ImmobilizedStatus() { Duration = ImmobilizeDuration });
    }

    public override bool Collate(PawnStatus other)
    {
        // Get outta town!
        if (other is HobbleStatus)
            return true;

        // Immobilized override hobbled - remove this status
        else if (other is ImmobilizedStatus)
            Expire();

        return false;
    }
}
