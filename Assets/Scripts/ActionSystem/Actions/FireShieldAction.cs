using System.Collections.Generic;

public class FireShieldAction : BattleAction
{
    protected override void Setup()
    {
        // Can only target self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Self),
        };

        // Affects adjacent cells
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Heals and buffs adjacent
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode() { Actor = Actor, Status = new PoisonStatus() { Duration = 3 } },
            new ApplyStatusNode() { Actor = Actor, Status = new InvulnerabilityStatus() { Duration = 1 } }
        };
    }
}
