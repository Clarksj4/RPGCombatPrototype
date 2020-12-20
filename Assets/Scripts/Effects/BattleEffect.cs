using UnityEngine;

/// <summary>
/// Encapsulates persistent battle effects - could be on a cell,
/// or on an actor.
/// </summary>
public abstract class BattleEffect
{
    /// <summary>
    /// Gets the range of this effect.
    /// </summary>
    public abstract int Range { get; }
    /// <summary>
    /// Gets the turn duration of this effect.
    /// </summary>
    public abstract int Duration { get; }
    /// <summary>
    /// Gets the actor that this effect is attached to, if any.
    /// </summary>
    public Actor Actor { get; protected set; }
    /// <summary>
    /// Gets the map that this effect originates from.
    /// </summary>
    public Formation OriginFormation { get; protected set; }
    /// <summary>
    /// Gets the coordinate that this effect originates from,
    /// if this effect is attached to an actor, this
    /// coordinate will be the actor's map position.
    /// </summary>
    public Vector2Int OriginPosition { get; protected set; }
    /// <summary>
    /// Checks if the actor is able to have this effect
    /// attached to it.
    /// </summary>
    public abstract bool IsActorAble(Actor actor);

    /// <summary>
    /// Sets the actor that this effect will be attached to
    /// if able. Returns true if the actor is able.
    /// </summary>
    public virtual bool SetActor(Actor actor)
    {
        bool isAble = IsActorAble(actor);
        if (isAble)
        {
            Actor = actor;
            OriginFormation = actor.Formation;
            OriginPosition = actor.Coordinate;
        }
        return isAble;
    }

    /// <summary>
    /// Checks whether the origin coordinate is valid.
    /// Returns true if it is.
    /// </summary>
    public abstract bool IsOriginValid(Formation formation, Vector2Int position);

    /// <summary>
    /// Sets the originating coordinate for this effect.
    /// Returns true if the coordinate was valid.
    /// </summary>
    public virtual bool SetOrigin(Formation formation, Vector2Int position)
    {
        bool isValid = IsOriginValid(formation, position);
        if (isValid)
        {
            OriginFormation = formation;
            OriginPosition = position;
        }
        return isValid;
    }
}
