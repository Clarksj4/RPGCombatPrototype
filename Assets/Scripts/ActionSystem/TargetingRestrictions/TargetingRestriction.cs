using UnityEngine;

public abstract class TargetingRestriction
{
    protected BattleAction action;

    public TargetingRestriction(BattleAction action)
    {
        this.action = action;
    }

    public abstract bool IsTargetValid(Cell cell); 
}
