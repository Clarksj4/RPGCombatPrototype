using System.Collections;
using System.Collections.Generic;
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
    public virtual int Range { get { return -1; } }
    /// <summary>
    /// Gets the actor who will perform this action.
    /// </summary>
    public Actor Actor { get; protected set; }
    /// <summary>
    /// Gets the map that this action is targeted to.
    /// </summary>
    public Formation TargetFormation { get; protected set; }
    /// <summary>
    /// Gets the coordinate that this action is targeting.
    /// </summary>
    public Vector2Int TargetPosition { get; protected set; }
    /// <summary>
    /// Gets the map where this action originates from.
    /// </summary>
    public Formation OriginFormation { get; protected set; }
    /// <summary>
    /// Gets the coordinate that this action originates from.
    /// </summary>
    public Vector2Int OriginPosition { get; protected set; }
    /// <summary>
    /// Gest a collection of informative tags about this action.
    /// </summary>
    public abstract ActionTag Tags { get; }
    /// <summary>
    /// Gets the applicable targets for this ability.
    /// </summary>
    public abstract Target Target { get; }
    /// <summary>
    /// Gets whether this action can target a formation other than
    /// the one the actor is currently on.
    /// </summary>
    public virtual FormationTarget FormationTarget { get { return FormationTarget.Self; } }
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
            OriginFormation = actor.Formation;
            OriginPosition = actor.GridPosition;
        }
        return isAble;
    }

    /// <summary>
    /// Sets the target for this action if valid. Returns true
    /// if the target is a valid one.
    /// </summary>
    public virtual bool SetTarget(Formation formation, Vector2Int position)
    {
        // Check if target is valid.
        bool isValid = IsTargetValid(formation, position);
        if (isValid)
        {
            // Set target map / position
            TargetFormation = formation;
            TargetPosition = position;
        }
        return isValid;
    }

    /// <summary>
    /// Removes the action's current target.
    /// </summary>
    public void DeselectTarget()
    {
        TargetFormation = null;
        TargetPosition = Vector2Int.zero;
    }

    /// <summary>
    /// Checks whether the given cell is a valid target.
    /// </summary>
    public virtual bool IsTargetValid(Formation formation, Vector2Int position)
    {
        return IsTargetFormationValid(formation) &&
                IsTargetTypeValid(formation, position) &&
                IsTargetInRange(formation, position);
    }

    /// <summary>
    /// Checks whether the thing in the targeted cell 
    /// is a valid target.
    /// </summary>
    public virtual bool IsTargetTypeValid(Formation formation, Vector2Int position)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (Target == Target.All)  return true;
        if (Target == Target.None) return false;

        // Assume the target is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        Pawn pawn = formation.GetPawnAtCoordinate(position);
        bool isSelf = pawn == Actor;
        bool pawnExists = pawn != null;
        bool isActorOnSameTeam = IsActorOnSameTeam(pawn);

        // Can target self.
        if (Target.HasFlag(Target.Self) &&
            pawnExists && isSelf)
            valid = true;

        // Can target allies.
        else if (Target.HasFlag(Target.Ally) &&
            pawnExists && isActorOnSameTeam)
            valid = true;

        // Can target enemies.
        else if (Target.HasFlag(Target.Enemy) &&
            pawnExists && !isActorOnSameTeam)
            valid = true;

        // Can target empty cells.
        else if (Target.HasFlag(Target.Area) &&
            !pawnExists)
            valid = true;
        
        return valid;
    }

    public virtual bool IsTargetFormationValid(Formation formation)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (FormationTarget == FormationTarget.All)  return true;
        if (FormationTarget == FormationTarget.None) return false;

        // Assume the formation is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        bool isSelfFormation = formation == Actor.Formation;

        // Can target own formation.
        if (FormationTarget.HasFlag(FormationTarget.Self) &&
            isSelfFormation)
            valid = true;

        // Can target other formations.
        else if (FormationTarget.HasFlag(FormationTarget.Other) &&
            !isSelfFormation)
            valid = true;

        return valid;
    }

    /// <summary>
    /// Checks whether the given position on the given formation is
    /// within this action's range.
    /// </summary>
    public virtual bool IsTargetInRange(Formation formation, Vector2Int position)
    {
        bool infiniteRange = Range < 0;
        bool positionInRange = formation.IsInRange(OriginPosition, position, Range);
        return infiniteRange || positionInRange;
    }

    /// <summary>
    /// Checks if the given actor is currently able to perform
    /// this action.
    /// </summary>
    public virtual bool IsActorAble(Actor actor)
    {
        return !actor.Incapacitated;
    }

    /// <summary>
    /// Gets whether the assigned actor is able to complete
    /// the action with its current parameters.
    /// </summary>
    public virtual bool CanDo()
    {
        return IsActorAble(Actor) &&
               IsTargetValid(TargetFormation, TargetPosition);
    }

    // TODO: handle being able to target multiple formations with an area attack
    /// <summary>
    /// Gets the coordinates that will be affected by this action.
    /// </summary>
    public virtual IEnumerable<Vector2Int> GetAffectedCoordinates()
    {
        yield return TargetPosition;
    }

    /// <summary>
    /// Performs this action. Returns true if the action was
    /// successful.
    /// </summary>
    public abstract IEnumerator Do();

    /// <summary>
    /// Checks if the given pawn is an actor who is on the same
    /// team as this action's actor.
    /// </summary>
    protected bool IsActorOnSameTeam(Pawn pawn)
    {
        if (pawn is Actor)
        {
            // Convert to actor and check its allegience.
            Actor actor = pawn as Actor;
            return actor.Team == Actor.Team;
        }

        // Pawn is not an actor - it doesn't have a team.
        return false;
    }
}
