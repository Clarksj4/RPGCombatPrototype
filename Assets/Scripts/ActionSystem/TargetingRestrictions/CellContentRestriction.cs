using UnityEngine;

public class CellContentRestriction : TargetingRestriction
{
    /// <summary>
    /// Gets or sets the type of content permitted in the
    /// targeted cells.
    /// </summary>
    public TargetableCellContent Content { get; set; }

    public override bool IsTargetValid(Cell cell)
    {
        bool isCellValid = IsTargetCellContentValid(cell);
        return isCellValid;
    }

    /// <summary>
    /// Checks whether the thing in the targeted cell is 
    /// a valid target.
    /// </summary>
    private bool IsTargetCellContentValid(Cell cell)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (Content == TargetableCellContent.All) return true;
        if (Content == TargetableCellContent.None) return false;

        // Assume the target is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        Pawn pawn = cell.Contents.FirstOfTypeOrDefault<IGridBased, Pawn>();
        bool isSelf = pawn == Actor;
        bool pawnExists = pawn != null;
        bool isActorOnSameTeam = IsActorOnSameTeam(pawn);

        // Can target self.
        if (Content.HasFlag(TargetableCellContent.Self) &&
            pawnExists && isSelf)
            valid = true;

        // Can target allies.
        else if (Content.HasFlag(TargetableCellContent.Ally) &&
            pawnExists && isActorOnSameTeam && !isSelf)
            valid = true;

        // Can target enemies.
        else if (Content.HasFlag(TargetableCellContent.Enemy) &&
            pawnExists && !isActorOnSameTeam)
            valid = true;

        // Can target empty cells.
        else if (Content.HasFlag(TargetableCellContent.Empty) &&
            !pawnExists)
            valid = true;

        return valid;
    }

    /// <summary>
    /// Checks if the given pawn is an actor who is on the same
    /// team as this action's actor.
    /// </summary>
    private bool IsActorOnSameTeam(Pawn actor)
    {
        return actor != null && 
               actor.Team == Actor.Team;
    }
}
