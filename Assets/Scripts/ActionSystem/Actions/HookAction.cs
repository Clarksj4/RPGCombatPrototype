using System.Collections.Generic;

public class HookAction : BattleAction
{
    public HookAction(Actor actor)
    : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Forced | ActionTag.Damage;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction(this),
            new AdjcentCellContentRestriction(this, TargetableCellContent.Empty, RelativeDirection.Towards),
            new CellContentRestriction(this, TargetableCellContent.Ally | TargetableCellContent.Enemy)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetActions = new List<ActionNode>()
        {
            new IsHitNode(this),
            new PushNode(this, 1, RelativeDirection.Towards),
            new DoDamageNode(this)
        };
    }
}
