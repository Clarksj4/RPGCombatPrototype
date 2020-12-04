using UnityEngine;
using System.Collections.Generic;

public class EmptyAdjacentCells : TargetableStrategy
{
    public EmptyAdjacentCells(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<(Formation, Vector2Int)> GetTargetableCells()
    {
        throw new System.NotImplementedException();
    }
}
