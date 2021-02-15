using System.Collections.Generic;

public class MartyrdomAction : BattleAction
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
            new CellContentRestriction() { Content = TargetableCellContent.Ally }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint()
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 2 }
        };

        // Take damage on behalf of ally
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode() { Status = new GuardedStatus() { Duration = 2 } }
        };
    }
}
