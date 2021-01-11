using UnityEngine;
using System.Collections.Generic;

public class AffectedRange : AffectedArea
{
    private int range;

    public AffectedRange(BattleAction action, int range)
        : base(action)
    {
        this.range = range;
    }

    public override IEnumerable<Cell> GetAffectedArea()
    {
        foreach (Cell cell in action.Grid.GetRange(action.TargetCell.Coordinate, range))
            yield return cell;
    }
}
