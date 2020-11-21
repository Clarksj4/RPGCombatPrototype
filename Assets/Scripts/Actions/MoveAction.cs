using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BattleAction
{
    public override int Range { get { return Actor.Movement; } }

    public override ActionTag[] Tags { get { return tags; } }
    private ActionTag[] tags = new ActionTag[] { ActionTag.Movement };

    public override bool IsActorAble(Actor actor)
    {
        return !actor.Incapacitated;
    }

    public override bool IsTargetValid(Formation formation, Vector2Int position)
    {
        bool isOnSameFormation = formation == OriginFormation;
        bool cellEmpty = formation.GetPawnAtCoordinate(position) == null;
        bool isInRange = formation.IsInRange(OriginPosition, position, Range);

        return isOnSameFormation &&
                cellEmpty &&
                isInRange;
    }

    public override bool Do()
    {
        bool canDo = IsActorAble(Actor) && IsTargetValid(TargetFormation, TargetPosition);

        if (canDo)
            Actor.SetCoordinate(TargetPosition);
        else
            Debug.Log($"Can't perform move action.");

        return canDo;
    }

    public override IEnumerable<Vector2Int> GetArea()
    {
        // TODO: actually do some path finding or something.
        yield return TargetPosition;
    }
}