using System.Collections.Generic;

public class PenanceAction : BattleAction
{
    protected override void Setup()
    {
        // Self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Self }
        };

        // Just the one target actually...
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Heal and stun self
        targetedActions = new List<ActionNode>()
        {
            new HealNode() { Actor = Actor, Amount = 20 },
            new ApplyStatusNode() { Actor = Actor, Status = new StunnedStatus() { Duration = 1 } },
            new ApplyStatusNode() { Actor = Actor, Status = new RenewStatus() { Duration = 1, HealPerTurn = 20 } }
        };
    }
}
