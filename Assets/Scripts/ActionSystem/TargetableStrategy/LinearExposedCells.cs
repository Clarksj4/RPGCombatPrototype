using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LinearExposedCells : LinearCells
{
    public LinearExposedCells(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override IEnumerable<Cell> GetTargetableCells()
    {
        IEnumerable<Cell> cells = base.GetTargetableCells();
        foreach (Cell target in cells)
        {
            // TODO: get direction from cell to origin of action
            Vector2 directionToOrigin = action.OriginCell.WorldPosition - target.WorldPosition;

            // TODO: clamp it so that the greatest axis is the one used
            // and the other axis is discarded
            Vector2 linearDirection = directionToOrigin.MaxAxisOnly();
            Vector2Int step = linearDirection.Reduce();

            // TODO: step through cells in that direction until the axis
            // we care about is equal to the origins value for that axis
            Vector2Int from = target.Coordinate * step.Abs();
            Vector2Int to = action.OriginCell.Coordinate * step.Abs();
            int nSteps = from.GetTravelDistance(to);

            // Check each cell between target and action origin to see
            // if its blocked
            bool exposed = true;
            for (int i = 1; i < nSteps; i++)
            {
                Vector2Int coordinate = target.Coordinate + (step * i);
                Cell cell = action.Grid.GetCell(coordinate);
                if (cell.IsOccupied<IDefender>())
                {
                    exposed = false; 
                    break;
                }
            }

            // Target not blocked - we gucci
            if (exposed)
                yield return target;
        }
    }
}
