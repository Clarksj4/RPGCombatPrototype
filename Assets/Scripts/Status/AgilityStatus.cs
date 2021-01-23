using UnityEngine;
using System;

[Serializable]
public class AgilityStatus : PawnStatus
{
    /// <summary>
    /// Gets the movement bonus granted to
    /// the targeted pawn.
    /// </summary>
    public int BonusMovement { get; set; }

    protected override void OnApplication()
    {
        base.OnApplication();

        // Apply bonus movement
        Pawn.Movement += BonusMovement;
    }

    protected override void OnExpired()
    {
        base.OnExpired();

        // Remove the movement again, but don't let movement fall below 0
        Pawn.Movement = Mathf.Max(0, Pawn.Movement - BonusMovement);
    }
}
