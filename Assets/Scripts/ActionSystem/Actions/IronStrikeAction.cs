using System.Collections.Generic;

public class IronStrikeAction : BattleAction
{
    protected override void Setup()
    {
        // Need to have enough health to cast it
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 },
            new RankCellsRestriction() { Ranks = new int[] { 0 } }
        };

        // Can only target self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Enemy },
            new RankCellsRestriction() { Ranks = new int[] { 0 } }
        };

        // Affects adjacent cells
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 },
            new ApplyStatusNode() { Status = new DefenseStatus() { Duration = 1 } }
        };

        // Heals and buffs adjacent
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new DoDamageNode() { BaseDamage = 25 }
        };
    }
}
