using UnityEngine;

public class CellContentRestriction : TargetableCellRestriction
{
    private TargetableCellContent targetableContent;

    public CellContentRestriction(BattleAction action, TargetableCellContent targetableContent)
        : base(action)
    {
        this.targetableContent = targetableContent;
    }

    public override bool IsTargetValid(Formation formation, Vector2Int coordinate)
    {
        bool isCellValid = IsTargetCellContentValid(formation, coordinate);
        return isCellValid;
    }

    /// <summary>
    /// Checks whether the thing in the targeted cell is 
    /// a valid target.
    /// </summary>
    public virtual bool IsTargetCellContentValid(Formation formation, Vector2Int position)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (targetableContent == TargetableCellContent.All) return true;
        if (targetableContent == TargetableCellContent.None) return false;

        // Assume the target is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        Pawn pawn = formation.GetPawnAtCoordinate(position);
        bool isSelf = pawn == action.Actor;
        bool pawnExists = pawn != null;
        bool isActorOnSameTeam = IsActorOnSameTeam(pawn);

        // Can target self.
        if (targetableContent.HasFlag(TargetableCellContent.Self) &&
            pawnExists && isSelf)
            valid = true;

        // Can target allies.
        else if (targetableContent.HasFlag(TargetableCellContent.Ally) &&
            pawnExists && isActorOnSameTeam)
            valid = true;

        // Can target enemies.
        else if (targetableContent.HasFlag(TargetableCellContent.Enemy) &&
            pawnExists && !isActorOnSameTeam)
            valid = true;

        // Can target empty cells.
        else if (targetableContent.HasFlag(TargetableCellContent.Empty) &&
            !pawnExists)
            valid = true;

        return valid;
    }

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
            return actor.Team == action.Actor.Team;
        }

        // Pawn is not an actor - it doesn't have a team.
        return false;
    }
}
