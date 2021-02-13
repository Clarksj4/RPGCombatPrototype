using System.Collections.Generic;

public class WindWalkAction : BattleAction
{
    protected override void Setup()
    {
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 }
        };

        // Target any ally
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Ally }
        };

        // Just the one ally actually...
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 }
        };

        // Move faster and evade an attack...
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode() { Status = new LinkedStatuses().Add(new EvasiveStatus() { Duration = 2, AttacksToEvade = 1 })
                                                                 .Add(new AgilityStatus() { Duration = 2, BonusMovement = 1 }) }
        };
    }
}
