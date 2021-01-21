using System.Collections.Generic;

public class BullRushAction : BattleAction
{
    public BullRushAction(Actor actor) 
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Damage | ActionTag.Forced;

        // Only usable from the back of the grid
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 2)
        };

        // Only targets enemies in the fron rank
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 0),
            new CellContentRestriction(this, TargetableCellContent.Enemy)
        };

        // Just the one target actually...
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Move to front of grid.
        beginningActions = new List<ActionNode>()
        {
            new PushNode(this) { Direction = Actor.Formation.Facing, Distance = 3 }
        };

        // Stomp em!
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this, 20),
            new PushNode(this) { RelativeDirection = RelativeDirection.Away, Distance = 3 }
        };
    }
}
