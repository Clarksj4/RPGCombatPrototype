using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AffectedRank : AffectedArea
{
    private int rank;

    public AffectedRank(BattleAction action, int rank = -1)
        : base(action) 
    {
        this.rank = rank;
    }

    public override IEnumerable<Cell> GetAffectedArea()
    {
        // Use the given rank, OR the same rank as the targeted cell
        Formation formation = action.TargetCell.Formation;
        int affectedRank = rank >= 0 ? rank : formation.GetRank(action.TargetCell.Coordinate);
        
        foreach (Cell cell in formation.GetRankCells(affectedRank))
            yield return cell;
    }
}
