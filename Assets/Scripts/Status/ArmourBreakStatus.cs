using UnityEngine;
using System.Collections;

public class ArmourBreakStatus : PawnStatus
{
    public ArmourBreakStatus(Pawn pawn, int duration)
        : base(pawn, duration) { /* Nothing! */ }

    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Defense -= 10;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Defense += 10;
    }
}
