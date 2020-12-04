using UnityEngine;

public abstract class TargetableCellRestriction
{
    protected BattleAction action;

    public TargetableCellRestriction(BattleAction action)
    {
        this.action = action;
    }

    public abstract bool IsTargetValid(Formation formation, Vector2Int coordinate);
}
