using System;

[Serializable]
public class PoisonStatus : PawnStatus
{
    protected override void DoEffect()
    {
        base.DoEffect();

        // Take 10% max health damage each tick.
        Pawn.TakeDamage((int)(Pawn.MaxHealth * 0.15f), false);
    }

    public override bool Collate(PawnStatus other)
    {
        // Can't stack poison damage - just extends the duration.
        if (other is PoisonStatus)
        {
            Duration += other.Duration;
            return true;
        }

        return false;
    }
}
