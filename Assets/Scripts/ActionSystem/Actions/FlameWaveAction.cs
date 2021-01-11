using System.Collections.Generic;

public class FlameWaveAction : BattleAction
{
    public FlameWaveAction()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Knowing what we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction(this, TargetableFormation.Other),
            new RankCells(this, 0)
        };

        targetedStrategy = new TargetedRank(this, 0);

        // Knowing what we do.
        actionSequence = new List<ActionNode>()
        {
            new DoDamageNode(this)
        };
    }
}
