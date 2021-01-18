using UnityEngine;
using System.Collections;

public class PoisonStatus : PawnStatus
{
    public PoisonStatus(Pawn pawn, int duration)
        : base(pawn, duration) { /* Nothing! */ }

    protected override void DoEffect()
    {
        base.DoEffect();

        Pawn.Health -= (int)(Pawn.MaxHealth * 0.1f);
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
