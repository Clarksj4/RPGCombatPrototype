using System.Collections.Generic;

public class FlameWaveAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Only usable from front rank
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 },
            new RankCellsRestriction() { Ranks = new int[] { 0 } }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Enemy },
            new RankCellsRestriction() { Ranks = new int[] { 0 } }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRank(this, 0)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new DoDamageNode() { BaseDamage = 25 }
        };
    }
}
