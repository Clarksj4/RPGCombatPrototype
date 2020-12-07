using UnityEngine;
using System.Linq;

public class EmptyAdjacentRestriction : TargetableCellRestriction
{
    private RelativeDirection directions;
    private bool allEmpty;

    public EmptyAdjacentRestriction(BattleAction action, RelativeDirection directions, bool allEmpty = true)
        : base(action) 
    {
        this.directions = directions;
        this.allEmpty = allEmpty;
    }

    public override bool IsTargetValid(Formation formation, Vector2Int coordinate)
    {
        bool valid;
        if (allEmpty)
            valid = AllAdjacentCellsEmpty(formation, coordinate);
        else
            valid = AnyAdjacentCellsEmpty(formation, coordinate);
        return valid;
    }

    private bool AllAdjacentCellsEmpty(Formation formation, Vector2Int coordinate)
    {
        return coordinate.GetRelativeDirections(action.Actor.GridPosition, directions)
                         .All(c => { return formation.GetPawnAtCoordinate(c) == null; });
    }

    private bool AnyAdjacentCellsEmpty(Formation formation, Vector2Int coordinate)
    {
        return coordinate.GetRelativeDirections(action.Actor.GridPosition, directions)
                         .Any(c => { return formation.GetPawnAtCoordinate(c) == null; });
    }
}
