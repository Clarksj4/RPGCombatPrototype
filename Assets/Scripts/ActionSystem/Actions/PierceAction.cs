using System.Collections.Generic;

public class PierceAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Only usable from the front rank
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 2 },
            new RankCellsRestriction() { Ranks = new int[] { 0 } }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FileCellsRestriction(),
            new ExposedCellsRestriction(),
            new CellContentRestriction() { Content = TargetableCellContent.Enemy }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedFile()
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 2 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new DoDamageNode() { BaseDamage = 30 }
        };

        // If one of the targets is not hit, then no subsequent targets are hit
        AffectedCellsIndependent = false;
    }
}
