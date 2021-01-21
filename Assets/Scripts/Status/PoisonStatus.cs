using UnityEngine;
using System.Collections;

public class PoisonStatus : PawnStatus
{
    public PoisonStatus(int duration)
        : base(duration) { /* Nothing! */ }

    protected override void DoEffect()
    {
        base.DoEffect();

        Pawn.TakeDamage((int)(Pawn.MaxHealth * 0.1f), false);
    }

    public override bool Collate(PawnStatus other)
    {
        if (other is PoisonStatus)
        {
            Duration += other.Duration;
            return true;
        }

        return false;
    }
}
