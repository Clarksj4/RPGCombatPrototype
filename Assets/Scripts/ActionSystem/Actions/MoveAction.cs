using System.Collections.Generic;

public class MoveAction : BattleAction
{
    public override int Range { get { return Actor.Movement; } }

    public MoveAction()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement;

        // Knowing what we can target
        targetableFormation = TargetableFormation.Self;
        targetableStrategy = new AnyCells(this);
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RangeRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Empty)
        };
        targetedStrategy = new TargetedPoint(this);

        // Knowing what we do.
        actionSequence = new List<ActionNode>()
        {
            new MoveActorNode(this)
        };
    }
}