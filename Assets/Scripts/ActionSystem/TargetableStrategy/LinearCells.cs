using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LinearCells : TargetableStrategy
{
    // TODO: Get rid of Vector2Ints - instead use coordinate system
    // that uses RANK and FILE values.
    // This makes the formation having a FRONT and FORWARD make sooo 
    // much more sense

    public LinearCells(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<(Formation, Vector2Int)> GetTargetableCells()
    {
        return GetRowCells().Concat(GetRankCells());
    }

    private IEnumerable<(Formation, Vector2Int)> GetRowCells()
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
                yield return (formation, coordinate);
        }
    }

    private IEnumerable<(Formation, Vector2Int)> GetRankCells()
    {
        int rank = action.Actor.Formation.GetRank(action.Actor.GridPosition);
        foreach (Vector2Int rankCell in action.Actor.Formation.GetRankCoordinates(rank))
            yield return (action.Actor.Formation, rankCell);
    }
}
