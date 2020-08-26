using UnityEngine;
using System.Collections;

public class AttackAction : BattleAction
{
    public override bool CanDo()
    {
        return true;
    }

    public override bool Do()
    {
        bool canDo = CanDo();

        if (canDo)
            Actor.Move(TargetPosition);

        return canDo;
    }

    public override bool IsValidTarget(BattleMap map, Vector2Int position)
    {
        return true;
    }
}
