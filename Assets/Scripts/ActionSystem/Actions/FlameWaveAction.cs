using System.Collections.Generic;

public class FlameWaveAction : BattleAction
{
    public FlameWaveAction()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Knowing what we can target
        targetableFormation = TargetableFormation.Other;
        targetableStrategy = new RankCells(this, 0);
        targetedStrategy = new TargetedRank(this, 0);

        // Knowing what we do.
        actionSequence = new List<ActionNode>()
        {
            new DoDamageNode(this)
        };
    }
}
