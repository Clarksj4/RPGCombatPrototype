using UnityEngine;

public abstract class TargetableCellRestriction
{
    protected BattleAction action;

    public TargetableCellRestriction(BattleAction action)
    {
        this.action = action;
    }

    public abstract bool IsTargetValid(Cell cell);
}
