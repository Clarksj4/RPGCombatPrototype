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
            new ManaRestriction() { Amount = 2 },
            new RankCellsRestriction() { Ranks = new int[] { 2 } }
        };

        // Only targets enemies in the fron rank
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Ranks = new int[] { 0 } },
            new CellContentRestriction() { Content = TargetableCellContent.Enemy }
        };

        // Just the one target actually...
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint()
        };

        // Move to front of grid.
        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 2 },
            new PushNode() { Direction = Actor.Formation.Facing, Distance = 3 }
        };

        // Stomp em!
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new DoDamageNode() { BaseDamage = 25 },
            new PushNode() { relativeDirection = RelativeDirection.Away, Distance = 3 }
        };
    }
}
