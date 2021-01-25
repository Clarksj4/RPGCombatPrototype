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
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 0 } }
        };

        // Can target enemies in the front rank
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 0 } },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Enemy }
        };

        // Affects only the targeted cell
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Swings, hits, and pushes away
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 30 },
            new PushNode() { Actor = Actor, RelativeDirection = RelativeDirection.Away, Distance = 1 }
        };
    }
}
