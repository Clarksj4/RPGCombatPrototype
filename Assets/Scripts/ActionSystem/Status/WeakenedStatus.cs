using System;

[Serializable]
public class WeakenedStatus : PawnStatus
{
    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Stats["Power"].Value -= 50;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Stats["Power"].Value += 50;
    }

    public override bool Collate(PawnStatus other)
    {
        // Can't stack weakness - just extend duration.
        if (other is WeakenedStatus)
        {
            Duration += other.Duration;
            return false;
        }

        return true;
    }

}
