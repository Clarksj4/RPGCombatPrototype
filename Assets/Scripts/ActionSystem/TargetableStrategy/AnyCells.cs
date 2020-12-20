using System.Collections.Generic;
using System.Linq;

public class AnyCells : TargetableStrategy
{
    public AnyCells(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<Cell> GetTargetableCells()
    {
        return action.GetTargetableFormations().SelectMany(f => f.GetCells());
    }
}
