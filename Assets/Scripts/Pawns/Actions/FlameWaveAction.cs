using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FlameWaveAction : BattleAction
{
    public override ActionTag Tags { get { return ActionTag.Damage | ActionTag.AoE; } }
    public override TargetableFormation TargetableFormation { get { return TargetableFormation.Other; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Empty | TargetableCellContent.Enemy; } }
    protected override TargetableCells TargetableCells { get { return targetableCells; } }
    private TargetableCells targetableCells;

    public FlameWaveAction()
    {
        targetableCells = new RankCells(this, 0);
    }

    public override IEnumerable<(Formation, Vector2Int)> GetAffectedCoordinates()
    {
        int rank = TargetFormation.GetRank(TargetPosition);
        return TargetFormation.GetRankCoordinates(rank).Select(c => { return (TargetFormation, c); });
    }

    public override IEnumerator Do()
    {
        foreach ((Formation formation, Vector2Int coordinate) in GetAffectedCoordinates())
        {
            Pawn pawn = formation.GetPawnAtCoordinate(coordinate);
            if (pawn != null)
                pawn.Health -= 10;
        }

        return null;
    }
}
