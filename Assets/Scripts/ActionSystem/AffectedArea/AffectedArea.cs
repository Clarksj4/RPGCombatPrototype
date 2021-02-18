using System;
using System.Collections.Generic;

[Serializable]
public abstract class AffectedArea : IBattleActionElement
{
    public string name { get { return GetType().Name; } }

    public abstract IEnumerable<Cell> GetAffectedArea(Cell targetedCell);
}
