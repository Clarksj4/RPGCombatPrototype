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
        return pawn != null && pawn.Health >= amount;
    }
}
