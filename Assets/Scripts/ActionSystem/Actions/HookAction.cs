using System.Collections.Generic;

public class HookAction : BattleAction
{
    public HookAction()
    : base()
    {
        Tags = ActionTag.Movement | ActionTag.Forced | ActionTag.Damage;

        // Knowing what we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction(this),
            new EmptyAdjacentRestriction(this, RelativeDirection.Towards),
            new CellContentRestriction(this, TargetableCellContent.Ally | TargetableCellContent.Enemy)
        };
        targetedStrategy = new TargetedPoint(this);

        // Knowing what we do.
        actionSequence = new List<ActionNode>()
        {
            new IsHitNode(this),
            new PushNode(this, 1, RelativeDirection.Towards),
            new DoDamageNode(this)
        };
    }
}
