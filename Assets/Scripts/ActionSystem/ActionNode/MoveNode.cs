using DG.Tweening;
using UnityEngine;

public class MoveNode : ActionNode
{
    public MoveNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool Do()
    {
        action.Actor.Move(Target);
        return true;
    }
}
