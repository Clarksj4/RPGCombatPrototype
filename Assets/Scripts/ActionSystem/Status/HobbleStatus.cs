using UnityEngine;
using System;

[Serializable]
public class HobbleStatus : PawnStatus
{
    [Tooltip("The duration that the target will be immobilized for after hobbled wears off.")]
    public int ImmobilizeDuration;

    public override PawnStatus Duplicate()
    {
        HobbleStatus duplicate = base.Duplicate() as HobbleStatus;
        duplicate.ImmobilizeDuration = ImmobilizeDuration;
        return duplicate;
    }

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
