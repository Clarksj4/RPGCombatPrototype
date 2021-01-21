using UnityEngine;
using System.Collections;
using System;

public class GuardedStatus : PawnStatus
{
    public Pawn Protector { get; private set; }

    public GuardedStatus(int duration, Pawn protector)
        : base(duration) 
    {
        Protector = protector;
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
