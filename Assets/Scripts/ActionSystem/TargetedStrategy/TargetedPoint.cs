using UnityEngine;
using System.Collections.Generic;

public class TargetedPoint : TargetedStrategy
{
    public TargetedPoint(BattleAction action)
        : base(action) { /* Nothing */ }

    public override IEnumerable<(Formation, Vector2Int)> GetAffectedCoordinates()
    {
        yield return (action.TargetFormation, action.TargetPosition);
    }
}
