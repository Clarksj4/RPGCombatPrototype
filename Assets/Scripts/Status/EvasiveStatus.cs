using UnityEngine;
using System.Collections;

public class EvasiveStatus : PawnStatus
{
    private int bonusEvasion;

    public EvasiveStatus(Pawn pawn, int duration, int bonusEvasion)
        : base(pawn, duration)
    {
        this.bonusEvasion = bonusEvasion;
    }

    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Evasion += bonusEvasion;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Evasion = Mathf.Max(0, Pawn.Evasion - bonusEvasion);
    }
}
