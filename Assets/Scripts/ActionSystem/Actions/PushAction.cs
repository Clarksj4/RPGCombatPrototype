using System.Collections.Generic;

public class PushAction : BattleAction
{
    public override int Range { get { return 1; } }

    public PushAction()
        : base()
    {
        Tags = ActionTag.Movement | ActionTag.Forced;

        // Knowing what we can target
        targetableFormation = TargetableFormation.Self;
        targetableStrategy = new AnyCells(this);
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RangeRestriction(this),
            new EmptyAdjacentRestriction(this, RelativeDirection.Away),
            new CellContentRestriction(this, TargetableCellContent.Ally | TargetableCellContent.Enemy)
        };
        targetedStrategy = new TargetedPoint(this);

        // Knowing what we do.
        actionSequence = new List<ActionNode>()
        {
            new PushNode(this, Range, RelativeDirection.Away)
        };
    }
}
