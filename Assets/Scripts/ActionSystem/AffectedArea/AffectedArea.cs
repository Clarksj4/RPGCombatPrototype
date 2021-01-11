using UnityEngine;
using System.Collections.Generic;

public abstract class AffectedArea
{
    protected BattleAction action;
    public AffectedArea(BattleAction action)
    {
        this.action = action;
    }

    public abstract IEnumerable<Cell> GetAffectedArea();
}
