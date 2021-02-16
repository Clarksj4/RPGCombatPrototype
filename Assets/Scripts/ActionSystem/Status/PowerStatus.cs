using UnityEngine;
using System;

[Serializable]
public class PowerStatus : PawnStatus
{
    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Power += 0.25f;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Power = Mathf.Max(0, Pawn.Power - 0.25f);
    }
}
