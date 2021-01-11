using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AffectedRank : AffectedArea
{
    private int rank;

    public AffectedRank(BattleAction action, int rank)
        : base(action) 
    {
        this.rank = rank;
    }

    public override IEnumerable<Cell> GetAffectedArea()
    {
        Formation formation = action.TargetCell.Formation;
        foreach (Cell cell in formation.GetRankCells(rank))
            yield return cell;
    }
}
