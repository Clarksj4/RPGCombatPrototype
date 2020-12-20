using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableStrategy
{
    protected BattleAction action;
    public TargetableStrategy(BattleAction action)
    {
        this.action = action;
    }

    public abstract IEnumerable<Cell> GetTargetableCells();
}