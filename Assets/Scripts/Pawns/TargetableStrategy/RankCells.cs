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

    public override IEnumerable<(Formation, Vector2Int)> GetTargetableCells()
    {
        foreach (Formation formation in action.GetTargetableFormations())
        {
            foreach (Vector2Int coordinate in formation.GetRankCoordinates(rank))
            {
                if (action.IsTargetValid(formation, coordinate))
                    yield return (formation, coordinate);
            }
        }
    }
}
