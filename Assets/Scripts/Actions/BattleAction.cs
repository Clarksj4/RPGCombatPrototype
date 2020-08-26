using UnityEngine;

public abstract class BattleAction
{
    public Actor Actor { get; protected set; }
    public BattleMap TargetMap { get; protected set; }
    public Vector2Int TargetPosition { get; protected set; }
    public BattleMap OriginMap { get; protected set; }
    public Vector2Int OriginPosition { get; protected set; }

    public virtual BattleAction SetActor(Actor actor)
    {
        Actor = actor;
        OriginMap = actor.BattleMap;
        OriginPosition = actor.MapPosition;
        return this;
    }

    public virtual BattleAction SetTarget(BattleMap map, Vector2Int position)
    {
        if (IsValidTarget(map, position))
        {
            TargetMap = map;
            TargetPosition = position;
        }

        return this;
    }

    public abstract bool IsValidTarget(BattleMap map, Vector2Int position);

    public abstract bool CanDo();

    public abstract bool Do();
}
