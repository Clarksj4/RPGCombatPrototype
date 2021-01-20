using UnityEngine;
using System.Collections;

public class HealthRestriction : TargetingRestriction
{
    private int amount;

    public HealthRestriction(BattleAction action, int amount)
        : base(action)
    {
        this.amount = amount;
    }

    public override bool IsTargetValid(Cell cell)
    {
        Pawn pawn = cell.GetContent<Pawn>();
        if (pawn != null)
        {
            if (amount >= 0)
                return pawn.Health >= amount;
            else
                return pawn.MaxHealth - pawn.Health >= Mathf.Abs(amount);
        }

        return true;
    }
}
