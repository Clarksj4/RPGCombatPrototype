using UnityEngine;
using System.Collections;


public class FormationRestriction : TargetingRestriction
{
    private TargetableFormation targets;

    public FormationRestriction(BattleAction action, TargetableFormation targets) 
        : base (action)
    {
        this.targets = targets;
    }

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
        if (targets == TargetableFormation.All) return true;
        if (targets == TargetableFormation.None) return false;

        // Assume the formation is invalid and then include
        // cases as it meets their requirements
        bool valid = false;
        bool isSelfFormation = formation == action.Actor.Formation;

        // Can target own formation.
        if (targets.HasFlag(TargetableFormation.Self) &&
            isSelfFormation)
            valid = true;

        // Can target other formations.
        else if (targets.HasFlag(TargetableFormation.Other) &&
            !isSelfFormation)
            valid = true;

        return valid;
    }
}
