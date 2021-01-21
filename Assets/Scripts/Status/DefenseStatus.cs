using UnityEngine;
using System.Collections;

public class DefenseStatus : PawnStatus
{
    public DefenseStatus(int duration)
        : base(duration) { /* Nothing! */ }

    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Defense += 5;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Defense -= 5;
    }
}
