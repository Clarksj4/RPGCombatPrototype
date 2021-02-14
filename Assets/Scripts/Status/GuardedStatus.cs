using UnityEngine;
using System;

[Serializable]
public class GuardedStatus : PawnStatus
{
    /// <summary>
    /// Gets the pawn that will take damage on behalf of
    /// the targeted pawn.
    /// </summary>
    public Pawn Protector;

    protected override void OnApplication()
    {
        Pawn.AddSurrogate(Protector);
    }

    protected override void OnExpired()
    {
        Pawn.RemoveSurrogate(Protector);
    }
}
