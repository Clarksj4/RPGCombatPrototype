﻿using System.Collections.Generic;
using UnityEngine;

public class PushAction : BattleAction
{
    /// <summary>
    /// The range of this push action.
    /// </summary>
    private const int RANGE = 1;
    /// <summary>
    /// The distance that the target will be pushed.
    /// </summary>
    private const int PUSH_DISTANCE = 1;

    public override int Range { get { return RANGE; } }

    public override ActionTag[] Tags { get { return tags; } }
    private ActionTag[] tags = new ActionTag[] { ActionTag.Movement, ActionTag.Forced };

    public override bool IsActorAble(Actor actor)
    {
        return !actor.Incapacitated;
    }

    public override bool IsTargetValid(BattleMap map, Vector2Int position)
    {
        Pawn target = map.GetPawnAtCoordinate(position);
        bool inRange = map.IsInRange(OriginPosition, position, Range);
        return target != null &&
                target != Actor &&
                inRange;
    }

    public override bool Do()
    {
        bool canDo = IsActorAble(Actor) && IsTargetValid(TargetMap, TargetPosition);

        if (canDo)
        {
            // Get direction to target
            Pawn target = TargetMap.GetPawnAtCoordinate(TargetPosition);
            Vector2Int direction = GetDirectionToTarget(target);

            // Push target in that direction
            target.SetCoordinate(target.MapPosition + (direction * PUSH_DISTANCE));
        }

        else
            Debug.Log("Can't perform push action.");

        return canDo;
    }

    private Vector2Int GetDirectionToTarget(Pawn target)
    {
        // Get direction to defender
        Vector2Int delta = target.MapPosition - Actor.MapPosition;
        Vector2Int direction = delta.Reduce();
        return direction;
    }

    public override IEnumerable<Vector2Int> GetArea()
    {
        yield return TargetPosition;
    }
}
