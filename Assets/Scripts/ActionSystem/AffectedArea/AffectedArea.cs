using System;
using System.Collections.Generic;

[Serializable]
public abstract class AffectedArea
{
    public abstract IEnumerable<Cell> GetAffectedArea(Cell targetedCell);
}
