using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SameRowExposedCells : TargetableStrategy
{
    public SameRowExposedCells(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<(Formation, Vector2Int)> GetTargetableCells()
    {
        // Need to consider all possible formations
        foreach (Formation formation in action.GetTargetableFormations())
        {
            // Get closest coordinate in front rank.
            Vector2Int closestCoordinate = formation.GetClosestCoordinate(action.Actor.WorldPosition);

            // Get direction AWAY from the closest coordinate in fron rank.
            Vector2Int directionCoordinate = Vector2Int.Scale(-formation.Forward, formation.NCells);

            // Get line of cells starting at front rank to the back of the grid.
            IEnumerable<Vector2Int> line = formation.GetCoordinatesInLine(closestCoordinate, directionCoordinate);
            foreach (Vector2Int coordinate in line)
            {
                yield return (formation, coordinate);

                // Only iterate until encountering the first occupied cell (inclusive)
                bool occupied = formation.GetPawnAtCoordinate(coordinate) != null;
                if (occupied)
                    break;
            }
        }
    }
}
