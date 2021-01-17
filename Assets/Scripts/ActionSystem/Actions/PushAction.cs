using System.Collections.Generic;

public class PushAction : BattleAction
{
    public override int Range { get { return 1; } }

    public PushAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Forced;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction(this, TargetableFormation.Self),
            new RangeRestriction(this),
            new AdjcentCellContentRestriction(this, TargetableCellContent.Empty, RelativeDirection.Away),
            new CellContentRestriction(this, TargetableCellContent.Ally | TargetableCellContent.Enemy)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new PushNode(this) { RelativeDirection = RelativeDirection.Away, Distance = Range }
        };
    }
}
