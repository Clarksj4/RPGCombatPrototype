using UnityEngine;
using System.Collections;

public class FlameWaveAction : BattleAction
{
    public override ActionTag Tags { get { return ActionTag.Damage | ActionTag.AoE; } }
    public override TargetableFormation TargetableFormation { get { return TargetableFormation.Other; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Empty | TargetableCellContent.Enemy; } }

    public FlameWaveAction()
    {
        targetableStrategy = new RankCells(this, 0);
        targetedStrategy = new TargetedRank(this);
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
