using UnityEngine;
using System;

[Serializable]
public class AgilityStatus : PawnStatus
{
    [Tooltip("The bonus movement to apply to the target.")]
    public int BonusMovement;

    public override PawnStatus Duplicate()
    {
        AgilityStatus duplicate = base.Duplicate() as AgilityStatus;
        duplicate.BonusMovement = BonusMovement;
        return duplicate;
    }

    protected override void OnApplication()
    {
        base.OnApplication();

        // Apply bonus movement
        Pawn.Stats["Movement"]?.Increment(BonusMovement);
    }

    protected override void OnExpired()
    {
        base.OnExpired();

        // Remove the movement again, but don't let movement fall below 0
        Pawn.Stats["Movement"]?.Decrement(BonusMovement);
    }
}
