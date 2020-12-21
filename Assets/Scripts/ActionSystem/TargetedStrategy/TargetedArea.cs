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
        foreach (Vector2Int coordinate in action.TargetFormation.GetCoordinatesInRange(action.TargetPosition, area))
            yield return (action.TargetFormation, coordinate);
    }
}
