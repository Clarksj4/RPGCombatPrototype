using System;
using UnityEngine;

[Serializable]
public class RenewStatus : PawnStatus
{
    public int HealPerTurn { get { return healPerTurn; } }
    [SerializeField]
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
