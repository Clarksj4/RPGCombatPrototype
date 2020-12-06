using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TargetedRank : TargetedStrategy
{
    public TargetedRank(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<(Formation, Vector2Int)> GetAffectedCoordinates()
    {
        int rank = action.TargetFormation.GetRank(action.TargetPosition);
        return action.TargetFormation.GetRankCoordinates(rank).Select(c => { 
            return (action.TargetFormation, c); 
        });
    }
}
