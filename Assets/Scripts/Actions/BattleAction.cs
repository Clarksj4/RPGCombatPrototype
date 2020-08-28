using UnityEngine;

/// <summary>
/// Encapsulates any action / ability used by an actor
/// during a battle.
/// </summary>
public abstract class BattleAction
{
    /// <summary>
    /// Gets the range of this action.
    /// </summary>
    public abstract int Range { get; }
    /// <summary>
    /// Gets the actor who will perform this action.
    /// </summary>
    public Actor Actor { get; protected set; }
    /// <summary>
    /// Gets the map that this action is targeted to.
    /// </summary>
    public BattleMap TargetMap { get; protected set; }
    /// <summary>
    /// Gets the coordinate that this action is targeting.
    /// </summary>
    public Vector2Int TargetPosition { get; protected set; }
    /// <summary>
    /// Gets the map where this action originates from.
    /// </summary>
    public BattleMap OriginMap { get; protected set; }
    /// <summary>
    /// Gets the coordinate that this action originates from.
    /// </summary>
    public Vector2Int OriginPosition { get; protected set; }
    /// <summary>
    /// Gest a collection of informative tags about this action.
    /// </summary>
    public abstract ActionTag[] Tags { get; }
    /// <summary>
    /// Sets the actor who will perform this action. Returns
    /// true if the actor is able to perform the action.
    /// </summary>
    public virtual bool SetActor(Actor actor)
    {
        // Check if actor is able to do action.
        bool isAble = IsActorAble(actor);
        if (isAble)
        {
            // Set actor and originating map / position.
            Actor = actor;
            OriginMap = actor.Map;
            OriginPosition = actor.MapPosition;
        }
        return isAble;
    }

    /// <summary>
    /// Sets the target for this action if valid. Returns true
    /// if the target is a valid one.
    /// </summary>
    public virtual bool SetTarget(BattleMap map, Vector2Int position)
    {
        // Check if target is valid.
        bool isValid = IsTargetValid(map, position);
        if (isValid)
        {
            // Set target map / position
            TargetMap = map;
            TargetPosition = position;
        }
        return isValid;
    }

    /// <summary>
    /// Checks whether the given target is valid.
    /// </summary>
    public abstract bool IsTargetValid(BattleMap map, Vector2Int position);

    /// <summary>
    /// Checks if the given actor is currently able to perform
    /// this action.
    /// </summary>
    public abstract bool IsActorAble(Actor actor);

    /// <summary>
    /// Performs this action. Returns true if the action was
    /// successful.
    /// </summary>
    public abstract bool Do();
}
