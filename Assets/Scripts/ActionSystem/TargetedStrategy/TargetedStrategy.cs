using UnityEngine;
using System.Collections.Generic;

public abstract class TargetedStrategy
{
    protected BattleAction action;
    public TargetedStrategy(BattleAction action)
    {
        this.action = action;
    }

    public abstract IEnumerable<Cell> GetAffectedCoordinates();
}
