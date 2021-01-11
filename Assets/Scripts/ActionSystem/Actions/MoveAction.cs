using System.Collections.Generic;

public class MoveAction : BattleAction
{
    public override int Range { get { return Actor.Movement; } }

    public MoveAction()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction(this, TargetableFormation.Self),
            new RangeRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Empty)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        actionSequence = new List<ActionNode>()
        {
            new MoveActorNode(this)
        };
    }
}