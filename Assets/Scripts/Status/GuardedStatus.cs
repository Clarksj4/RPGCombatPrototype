using UnityEngine;
using System;

[Serializable]
public class GuardedStatus : PawnStatus
{
    /// <summary>
    /// Gets the pawn that will take damage on behalf of
    /// the targeted pawn.
    /// </summary>
    public Pawn Protector { get { return protector; } }
    [SerializeField]
    private Pawn protector;

    public GuardedStatus(int duration, Pawn protector)
        : base(duration) 
    {
        this.protector = protector;
    }

    protected override void OnApplication()
    {
        Pawn.AddSurrogate(Protector);
    }

    protected override void OnExpired()
    {
        Pawn.RemoveSurrogate(Protector);
    }
}
