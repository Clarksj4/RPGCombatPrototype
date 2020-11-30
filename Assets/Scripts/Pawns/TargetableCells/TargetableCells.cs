using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableCells
{
    protected BattleAction action;
    public TargetableCells(BattleAction action)
    {
        this.action = action;
    }

    public abstract IEnumerable<(Formation, Vector2Int)> GetTargetableCells();
}