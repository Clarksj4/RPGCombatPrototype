
public class FormationRestriction : TargetingRestriction
{
    /// <summary>
    /// Gets or sets the formations that are valid targets.
    /// </summary>
    public TargetableFormation Formations { get; set; }

    public override bool IsTargetValid(Cell cell)
    {
        return IsTargetFormationValid(cell.Formation);
    }

    /// <summary>
    /// Checks whether the given formation is a valid target.
    /// </summary>
    private bool IsTargetFormationValid(Formation formation)
    {
        // Check for all or nothing cases first to see
        // if we can skip the other checks.
        if (Formations == TargetableFormation.All) return true;
        if (Formations == TargetableFormation.None) return false;

        // Assume the formation is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        bool isSelfFormation = formation == Actor.Formation;

        // Can target own formation.
        if (Formations.HasFlag(TargetableFormation.Self) &&
            isSelfFormation)
            valid = true;

        // Can target other formations.
        else if (Formations.HasFlag(TargetableFormation.Other) &&
            !isSelfFormation)
            valid = true;

        return valid;
    }
}
