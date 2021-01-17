using UnityEngine;

public abstract class ActionNode
{
    /// <summary>
    /// Gets or sets the cell that this action will target.
    /// </summary>
    public Cell Target { get; set; }

    protected BattleAction action;

    public ActionNode(BattleAction action)
    {
        this.action = action;
    }

    public abstract bool Do();
}
