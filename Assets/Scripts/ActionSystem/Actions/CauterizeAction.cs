using System.Collections.Generic;

public class CauterizeAction : BattleAction
{
    protected override void Setup()
    {
        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Ally)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new HealNode() { Actor = Actor, Amount = 20 },
            new ApplyStatusNode() { Actor = Actor, Status = new VulnerabilityStatus() { Duration = 2 } }
        };
    }
}
