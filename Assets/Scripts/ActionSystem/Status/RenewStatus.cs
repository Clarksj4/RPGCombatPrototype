using System;
using UnityEngine;

[Serializable]
public class RenewStatus : PawnStatus
{
    [Tooltip("The amount of health to heal per turn.")]
    public int HealPerTurn;

    public override PawnStatus Duplicate()
    {
        RenewStatus duplicate = base.Duplicate() as RenewStatus;
        duplicate.HealPerTurn = HealPerTurn;
        return duplicate;
    }

    protected override void DoEffect()
    {
        base.DoEffect();
        Pawn.Stats["Health"].Value += HealPerTurn;
    }
}
