using UnityEngine;
using System.Collections.Generic;

public class AffectedRange : AffectedArea
{
    private int min;
    private int max;

    public AffectedRange(BattleAction action, int min, int max)
        : base(action)
    {
        this.min = min;
        this.max = max;
    }

    public override IEnumerable<Cell> GetAffectedArea()
    {
        foreach (Cell cell in action.Grid.GetRange(action.TargetCell.Coordinate, min, max))
            yield return cell;
    }
}
