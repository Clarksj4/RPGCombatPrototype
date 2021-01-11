using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetedRank : TargetedStrategy
{
    private int rank;

    public TargetedRank(BattleAction action, int rank)
        : base(action) 
    {
        this.rank = rank;
    }

    public override IEnumerable<Cell> GetAffectedCoordinates()
    {
        foreach (Formation formation in action.GetTargetableFormations())
        {
            foreach (Cell cell in formation.GetRankCells(rank))
                yield return cell;
        }
    }
}
