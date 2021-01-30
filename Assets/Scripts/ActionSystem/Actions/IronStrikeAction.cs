using System.Collections.Generic;

public class IronStrikeAction : BattleAction
{
    protected override void Setup()
    {
        // Need to have enough health to cast it
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 0 } }
        };

        // Can only target self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Enemy },
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 0 } }
        };

        // Affects adjacent cells
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        selfActions = new List<ActionNode>()
        {
            new ApplyStatusNode() { Actor = Actor, Target = Actor.Cell, Status = new DefenseStatus() { Duration = 1 } }
        };

        // Heals and buffs adjacent
        targetedActions = new List<ActionNode>()
        {
            
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 25 }
        };
    }
}
