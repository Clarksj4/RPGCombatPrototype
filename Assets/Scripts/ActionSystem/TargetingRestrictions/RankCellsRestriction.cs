using UnityEngine;
using System.Collections.Generic;

public class RankCellsRestriction : TargetingRestriction
{
    private int rank;

    public RankCellsRestriction(BattleAction action, int rank)
        : base(action)
    {
        this.rank = rank;
    }

    public override bool IsTargetValid(Cell cell)
    {
        return cell.Formation.GetRank(cell.Coordinate) == rank;
    }
}
