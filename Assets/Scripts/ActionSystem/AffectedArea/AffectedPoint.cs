using System.Collections.Generic;

public class AffectedPoint : AffectedArea
{
    public override IEnumerable<Cell> GetAffectedArea(Cell targetedCell)
    {
        yield return targetedCell;
    }
}
