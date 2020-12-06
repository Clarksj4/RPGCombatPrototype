using System.Collections.Generic;

public class FlameWaveAction : BattleAction
{
    public override ActionTag Tags { get { return ActionTag.Damage | ActionTag.AoE; } }
    public override TargetableFormation TargetableFormation { get { return TargetableFormation.Other; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Empty | TargetableCellContent.Enemy; } }

    public FlameWaveAction()
    {
        targetableStrategy = new RankCells(this, 0);
        targetedStrategy = new TargetedRank(this);

        actionSequence = new List<ActionNode>()
        {
            new DoDamageNode(this)
        };
    }
}
