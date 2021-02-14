using System.Collections.Generic;

public class AffectedFile : AffectedArea
{
    public override IEnumerable<Cell> GetAffectedArea(Cell targetedCell)
    {
        Formation formation = targetedCell.Formation;
        int file = formation.GetFile(targetedCell.Coordinate);
        foreach (Cell cell in formation.GetFileCells(file))
            yield return cell;
    }
}
