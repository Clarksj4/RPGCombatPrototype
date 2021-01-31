using System.Collections.Generic;

public class PenanceAction : BattleAction
{
    protected override void Setup()
    {
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 }
        };

        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Self }
        };

        // Just the one target actually...
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 },
            new HealNode() { Amount = 20 },
            new ApplyStatusNode() { Status = new StunnedStatus() { Duration = 1 } },
            new ApplyStatusNode() { Status = new RenewStatus() { Duration = 1, HealPerTurn = 20 } }
        };
    }
}
