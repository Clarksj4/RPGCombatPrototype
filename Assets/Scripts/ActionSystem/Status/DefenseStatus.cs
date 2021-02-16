using System;

[Serializable]
public class DefenseStatus : PawnStatus
{
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
