using System;
using UnityEngine;

[Serializable]
public class RenewStatus : PawnStatus
{
    public int HealPerTurn { get; set; }

    protected override void DoEffect()
    {
        base.DoEffect();
        Pawn.GainHealth(HealPerTurn);
    }
}
