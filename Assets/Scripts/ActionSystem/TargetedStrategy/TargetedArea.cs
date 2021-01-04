using UnityEngine;
using System.Collections.Generic;

public class TargetedArea : TargetedStrategy
{
    private int area;

    public TargetedArea(BattleAction action, int area)
        : base(action)
    {
        this.area = area;
    }

    public override IEnumerable<Cell> GetAffectedCoordinates()
    {
        foreach (Cell cell in action.Grid.GetRange(action.TargetPosition, area))
            yield return cell;
    }
}
