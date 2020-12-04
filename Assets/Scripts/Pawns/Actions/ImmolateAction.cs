using System.Collections.Generic;

public class ImmolateAction : BattleAction
{
    /// <summary>
    /// The area from the targeted cell that will be affected.
    /// </summary>
    private const int AREA = 1;

    public override ActionTag Tags { get { return ActionTag.Damage | ActionTag.AoE; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Enemy; } }
    public override TargetableFormation TargetableFormation { get { return TargetableFormation.Other; } }

    public ImmolateAction()
        : base()
    {
        targetableStrategy = new LinearExposedCells(this);
        targetedStrategy = new TargetedArea(this, AREA);

        actionSequence = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this)
        };
    }
}
