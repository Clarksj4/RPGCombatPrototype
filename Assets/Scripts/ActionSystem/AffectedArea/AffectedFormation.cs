using System.Collections.Generic;

public class AffectedFormation : AffectedArea
{
    public override IEnumerable<Cell> GetAffectedArea(Cell targetedCell)
    {
        return targetedCell.Formation.GetCells();
    }
}
