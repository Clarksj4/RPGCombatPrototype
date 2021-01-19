using UnityEngine;
using System.Collections;

public class EvasiveStatus : PawnStatus
{
    public int BonusEvasion { get; set; }

    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Evasion += BonusEvasion;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Evasion = Mathf.Max(0, Pawn.Evasion - BonusEvasion);
    }
}
