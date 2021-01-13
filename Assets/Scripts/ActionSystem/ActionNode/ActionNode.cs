using UnityEngine;

public abstract class ActionNode
{
    protected BattleAction action;

    public ActionNode(BattleAction action)
    {
        this.action = action;
    }

    public abstract bool ApplyToCell(Cell originCell, Cell targetCell);
}
