using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AnyCells : TargetableStrategy
{
    public AnyCells(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<(Formation, Vector2Int)> GetTargetableCells()
    {
        // Return ALL the cells on all targetable formations.
        foreach (Formation formation in action.GetTargetableFormations())
            return formation.GetCoordinates()
                            .Select(c => (formation, c));
                            
        return null;
    }
}
