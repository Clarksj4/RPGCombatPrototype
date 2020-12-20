using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LinearCells : TargetableStrategy
{
    public LinearCells(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<Cell> GetTargetableCells()
    {
        return action.Grid.GetRowCells(action.OriginPosition.y).Concat(
                action.Grid.GetColumnCells(action.OriginPosition.x))
                .Where(TargetableFormationsContain);
    }

    private bool TargetableFormationsContain(Cell cell)
    {
        return action.GetTargetableFormations().Any(f => f.Contains(cell));
    }
}
