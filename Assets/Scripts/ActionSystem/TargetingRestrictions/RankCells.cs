using UnityEngine;
using System.Collections.Generic;

public class RankCells : TargetingRestriction
{
    private int rank;

    public RankCells(BattleAction action, int rank)
        : base(action)
    {
        this.rank = rank;
    }

    public override bool IsTargetValid(Cell cell)
    {
        return cell.Formation.GetRank(cell.Coordinate) == rank;
    }
}
