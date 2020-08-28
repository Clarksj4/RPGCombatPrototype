using UnityEngine;

public abstract class BattleAction
{
    public abstract int Range { get; }
    public Actor Actor { get; protected set; }
    public BattleMap TargetMap { get; protected set; }
    public Vector2Int TargetPosition { get; protected set; }
    public BattleMap OriginMap { get; protected set; }
    public Vector2Int OriginPosition { get; protected set; }

    public virtual bool SetActor(Actor actor)
    {
        bool isAble = IsActorAble(actor);
        if (isAble)
        {
            Actor = actor;
            OriginMap = actor.Map;
            OriginPosition = actor.MapPosition;
        }
        return isAble;
    }

    public virtual bool SetTarget(BattleMap map, Vector2Int position)
    {
        bool isValid = IsTargetValid(map, position);
        if (isValid)
        {
            TargetMap = map;
            TargetPosition = position;
        }
        return isValid;
    }

    public abstract bool IsTargetValid(BattleMap map, Vector2Int position);

    public abstract bool IsActorAble(Actor actor);

    public abstract bool Do();
}
