using UnityEngine;
using System.Collections;

public class EvasiveStatus : PawnStatus
{
    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Evasive = true;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Evasive = false;
    }
}
