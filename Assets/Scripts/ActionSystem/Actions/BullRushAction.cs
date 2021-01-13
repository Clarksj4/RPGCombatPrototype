using System.Collections.Generic;

public class BullRushAction : BattleAction
{
    public BullRushAction(Actor actor) 
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Damage | ActionTag.Forced;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FileCellsRestriction(this, Actor.File),
            new ExposedCellsRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Enemy)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetActions = new List<ActionNode>()
        {
            // TODO: move actor as far forward as possible.
            // TODO: move target as far back as possible.
            new PushNode(this, 3, RelativeDirection.Away)
        };
    }
}
