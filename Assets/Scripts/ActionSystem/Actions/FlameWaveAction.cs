using System.Collections.Generic;

public class FlameWaveAction : BattleAction
{
    public FlameWaveAction()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction(this, TargetableFormation.Other),
            new RankCellsRestriction(this, 0)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRank(this, 0)
        };

        // The effect upon those cells.
        actionSequence = new List<ActionNode>()
        {
            new DoDamageNode(this)
        };
    }
}
