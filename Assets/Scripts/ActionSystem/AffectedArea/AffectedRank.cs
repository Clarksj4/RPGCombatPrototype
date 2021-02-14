using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AffectedRank : AffectedArea
{
    public override IEnumerable<Cell> GetAffectedArea(Cell targetedCell)
    {
        // Use the given rank, OR the same rank as the targeted cell
        Formation formation = targetedCell.Formation;
        int rank = formation.GetRank(targetedCell.Coordinate);
        foreach (Cell cell in formation.GetRankCells(rank))
            yield return cell;
    }
}
