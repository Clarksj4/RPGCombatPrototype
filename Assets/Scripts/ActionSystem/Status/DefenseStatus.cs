using System;

[Serializable]
public class DefenseStatus : PawnStatus
{
    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Stats["Defense"]?.Increment(5);
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Stats["Defense"]?.Decrement(5);
    }
}
