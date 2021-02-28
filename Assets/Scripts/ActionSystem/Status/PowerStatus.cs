using UnityEngine;
using System;

[Serializable]
public class PowerStatus : PawnStatus
{
    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Stats["Power"]?.Increment(25);
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Stats["Power"]?.Decrement(25);
    }
}
