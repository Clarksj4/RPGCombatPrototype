using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RankCellsRestriction : TargetingRestriction
{
    private int[] ranks;

    public RankCellsRestriction(BattleAction action, params int[] ranks)
        : base(action)
    {
        this.ranks = ranks;
    }

    public override bool IsTargetValid(Cell cell)
    {
        return ranks.Contains(cell.Formation.GetRank(cell.Coordinate));
    }
}
