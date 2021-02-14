using System.Collections.Generic;

public abstract class AffectedArea
{
    public abstract IEnumerable<Cell> GetAffectedArea(Cell targetedCell);
}
