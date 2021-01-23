using System;

[Serializable]
public class PoisonStatus : PawnStatus
{
    public PoisonStatus(int duration)
        : base(duration) { /* Nothing! */ }

    protected override void DoEffect()
    {
        base.DoEffect();

        // Take 10% max health damage each tick.
        Pawn.TakeDamage((int)(Pawn.MaxHealth * 0.1f), false);
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
