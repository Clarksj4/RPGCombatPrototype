using System.Collections.Generic;

public class BullRushAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Damage | ActionTag.Forced;

        // Only usable from the back of the grid
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 2 } }
        };

        // Only targets enemies in the fron rank
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 0 } },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Enemy }
        };

        // Just the one target actually...
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Move to front of grid.
        beginningActions = new List<ActionNode>()
        {
            new PushNode() 
            { 
                Actor = Actor,
                Direction = Actor.Formation.Facing, 
                Distance = 3 
            }
        };

        // Stomp em!
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 25 },
            new PushNode() { Actor = Actor, RelativeDirection = RelativeDirection.Away, Distance = 3 }
        };
    }
}
