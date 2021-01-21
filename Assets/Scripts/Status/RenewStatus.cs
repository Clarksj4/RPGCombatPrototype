using UnityEngine;
using System.Collections;

public class RenewStatus : PawnStatus
{
    private int healPerTurn;

    public RenewStatus(int duration, int healPerTurn)
        : base(duration)
    {
        this.healPerTurn = healPerTurn;
    }

    protected override void DoEffect()
    {
        base.DoEffect();
        Pawn.GainHealth(healPerTurn);
    }
}
