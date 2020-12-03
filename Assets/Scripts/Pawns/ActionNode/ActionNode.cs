using UnityEngine;

public abstract class ActionNode : MonoBehaviour
{
    protected BattleAction action;

    public ActionNode(BattleAction action)
    {
        this.action = action;
    }

    public abstract bool ApplyToCell(Formation formation, Vector2Int position);
}
