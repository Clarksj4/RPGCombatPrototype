using System.Collections.Generic;

public class PoisonDartAction : BattleAction
{
    protected override void Setup()
    {
        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction() { Actor = Actor },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Enemy },
            new ExposedCellsRestriction() { Actor = Actor }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 15 },
            new ApplyStatusNode() { Actor = Actor, Status = new PoisonStatus() { Duration = 2 } }
        };
    }
}
