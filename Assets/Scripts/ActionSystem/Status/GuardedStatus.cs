using System;

[Serializable]
public class GuardedStatus : PawnStatus
{
    protected override void OnApplication()
    {
        Pawn.AddSurrogate(Applicator);
    }

    protected override void OnExpired()
    {
        Pawn.RemoveSurrogate(Applicator);
    }
}
