using UnityEngine;
using System.Collections;

public class PushAction : BattleAction
{
    private const int RANGE = 1;
    private const int PUSH_DISTANCE = 1;

    public override int Range { get { return RANGE; } }

    public override bool IsActorAble(Actor actor)
    {
        return !actor.Incapacitated;
    }

    public override bool IsTargetValid(BattleMap map, Vector2Int position)
    {
        return map.GetPawnAtCoordinate(position) != null &&
                map.IsInRange(OriginPosition, position, Range);
    }

    public override bool Do()
    {
        bool canDo = IsActorAble(Actor) && IsTargetValid(TargetMap, TargetPosition);

        if (canDo)
        {
            // Get defender, check if hit, and apply damage
            Pawn defender = TargetMap.GetPawnAtCoordinate(TargetPosition);

            // Get direction to defender
            Vector2Int delta = defender.MapPosition - Actor.MapPosition;
            Vector2Int direction = delta.Reduce();

            // Push defender in that direction
            defender.SetCoordinate(defender.MapPosition + (direction * PUSH_DISTANCE));
        }

        else
            Debug.Log("Can't perform push action.");

        return canDo;
    }
}
