using System;

[Serializable]
public class PoisonStatus : PawnStatus
{
    /// <summary>
    /// All instances of poison deal the same percent max health damage.
    /// </summary>
    private const float percentMaxHealthDamagePerTurn = 0.1f;

    protected override void DoEffect()
    {
        base.DoEffect();

        // Take damage as a percent of max health each tick.
        Pawn.TakeDamage((int)(Pawn.Stats["Health"].Max * percentMaxHealthDamagePerTurn), false);
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
