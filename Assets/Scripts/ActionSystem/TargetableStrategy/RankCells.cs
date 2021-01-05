using UnityEngine;
using System.Collections.Generic;

public class RankCells : TargetableStrategy
{
    private int rank;

    public RankCells(BattleAction action, int rank)
        : base(action)
    {
        this.rank = rank;
    }

    public override IEnumerable<Cell> GetTargetableCells()
    {
        foreach (Formation formation in action.GetTargetableFormations())
        {
            foreach (Cell cell in formation.GetRankCells(action.OriginCell.Coordinate, rank))
                yield return cell;
        }
    }
}
