using System.Collections.Generic;

public class SmashAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Actor must be in the front rank to use this ability
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 },
            new RankCellsRestriction() { Ranks = new int[] { 0 } }
        };

        // Can target enemies in the front rank
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Ranks = new int[] { 0 } },
            new CellContentRestriction() { Content = TargetableCellContent.Enemy }
        };

        // Affects only the targeted cell
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 }
        };

        // Swings, hits, and pushes away
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new DoDamageNode() { BaseDamage = 20 },
            new PushNode() { RelativeDirection = RelativeDirection.Away, Distance = 3 }
        };
    }
}
