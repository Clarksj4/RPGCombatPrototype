using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public abstract TargetableCellContent TargetableCellContent { get; }
    /// <summary>
    /// Gets whether this action can target a formation other than
    /// the one the actor is currently on.
    /// </summary>
    public virtual TargetableFormation TargetableFormation { get { return TargetableFormation.Self; } }
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
    /// Gets all the formations that are valid targets for
    /// this action.
    /// </summary>
    public IEnumerable<Formation> GetPossibleTargetFormations()
    {
        return BattleManager.Instance.Formations.Where(IsTargetFormationValid);
    }

    /// <summary>
    /// Checks whether the given cell is a valid target.
    /// </summary>
    public virtual bool IsTargetValid(Formation formation, Vector2Int position)
    {
        return IsTargetFormationValid(formation) &&
                IsTargetCellContentValid(formation, position) &&
                IsTargetInRange(formation, position);
    }

    /// <summary>
    /// Checks whether the thing in the targeted cell 
    /// is a valid target.
    /// </summary>
    public virtual bool IsTargetCellContentValid(Formation formation, Vector2Int position)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (TargetableCellContent == TargetableCellContent.All)  return true;
        if (TargetableCellContent == TargetableCellContent.None) return false;

        // Assume the target is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        Pawn pawn = formation.GetPawnAtCoordinate(position);
        bool isSelf = pawn == Actor;
        bool pawnExists = pawn != null;
        bool isActorOnSameTeam = IsActorOnSameTeam(pawn);

        // Can target self.
        if (TargetableCellContent.HasFlag(TargetableCellContent.Self) &&
            pawnExists && isSelf)
            valid = true;

        // Can target allies.
        else if (TargetableCellContent.HasFlag(TargetableCellContent.Ally) &&
            pawnExists && isActorOnSameTeam)
            valid = true;

        // Can target enemies.
        else if (TargetableCellContent.HasFlag(TargetableCellContent.Enemy) &&
            pawnExists && !isActorOnSameTeam)
            valid = true;

        // Can target empty cells.
        else if (TargetableCellContent.HasFlag(TargetableCellContent.Empty) &&
            !pawnExists)
            valid = true;
        
        return valid;
    }

    public virtual bool IsTargetFormationValid(Formation formation)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (TargetableFormation == TargetableFormation.All)  return true;
        if (TargetableFormation == TargetableFormation.None) return false;

        // Assume the formation is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        bool isSelfFormation = formation == Actor.Formation;

        // Can target own formation.
        if (TargetableFormation.HasFlag(TargetableFormation.Self) &&
            isSelfFormation)
            valid = true;

        // Can target other formations.
        else if (TargetableFormation.HasFlag(TargetableFormation.Other) &&
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
    public virtual IEnumerable<(Formation, Vector2Int)> GetAffectedCoordinates()
    {
        yield return (TargetFormation, TargetPosition);
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
