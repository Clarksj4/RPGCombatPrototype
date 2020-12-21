using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetedLine : TargetedStrategy
{
    private RelativeDirection directions;
    private int range;

    public TargetedLine(BattleAction action, RelativeDirection directions, int range)
        : base(action) 
    {
        this.directions = directions;
        this.range = range;
    }

    public override IEnumerable<Cell> GetAffectedCoordinates()
    {
        // Get step in each of the given directions
        IEnumerable<Vector2Int> relativeSteps = action.TargetPosition.GetRelativeDirections(action.OriginPosition, directions);
        foreach (Vector2Int relativeStep in relativeSteps)
        {
            // Foreach direction mark out a line of cells that extends up to range.
            foreach (Cell cell in action.Grid.GetLineCells(action.OriginCell, relativeStep, range))
                yield return cell;
        }
    }
}
