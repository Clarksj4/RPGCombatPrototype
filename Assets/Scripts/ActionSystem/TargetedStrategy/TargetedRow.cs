using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TargetedRow : TargetedStrategy
{
    public TargetedRow(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<(Formation, Vector2Int)> GetAffectedCoordinates()
    {
        int row = action.TargetFormation.GetRow(action.TargetPosition);
        return action.TargetFormation.GetRowCoordinates(row).Select(c => {
            return (action.TargetFormation, c);
        });
    }
}
