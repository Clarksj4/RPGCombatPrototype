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

        // Things to do before affecting the cells.
        beginningActions = new List<ActionNode>()
        {
            new PushNode(this) { Direction = Actor.Formation.Facing, Distance = 3 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new PushNode(this) { RelativeDirection = RelativeDirection.Away, Distance = 3 }
        };
    }
}
