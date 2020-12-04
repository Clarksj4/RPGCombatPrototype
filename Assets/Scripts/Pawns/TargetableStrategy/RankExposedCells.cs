using UnityEngine;
using System.Collections.Generic;

public class RankExposedCells : TargetableStrategy
{
    public RankExposedCells(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<(Formation, Vector2Int)> GetTargetableCells()
    {
        foreach (Formation formation in action.GetTargetableFormations())
        {
            // Get direction AWAY from front rank.
            Vector2Int directionCoordinate = Vector2Int.Scale(-formation.Forward, formation.NCells);

            foreach (Vector2Int frontRank in formation.GetFrontRankCoordinates())
            {
                // Get line of cells starting at front rank to the back of the grid.
                IEnumerable<Vector2Int> line = formation.GetCoordinatesInLine(frontRank, directionCoordinate);
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
}
