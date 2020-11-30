﻿using UnityEngine;
using System.Collections.Generic;

public class RankCells : TargetableCells
{
    private int rank;

    public RankCells(BattleAction action, int rank)
        : base(action)
    {
        this.rank = rank;
    }

    public override IEnumerable<(Formation, Vector2Int)> GetTargetableCells()
    {
        foreach (Formation formation in action.GetPossibleTargetFormations())
        {
            foreach (Vector2Int coordinate in formation.GetRankCoordinates(rank))
                yield return (formation, coordinate);
        }
    }
}