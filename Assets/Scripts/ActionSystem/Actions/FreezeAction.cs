using System.Collections.Generic;

public class FreezeAction : BattleAction
{
    protected override void Setup()
    {
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 2 }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Enemy },
            new ExposedCellsRestriction()
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint()
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new DoDamageNode() { BaseDamage = 15 },
            new ApplyStatusNode() { Status = new ImmobilizedStatus() { Duration = 2 } }
        };
    }
}
