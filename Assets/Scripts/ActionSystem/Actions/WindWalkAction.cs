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

        // Instantiate instances separately because need to link em.
        EvasiveStatus evasive = new EvasiveStatus() { Duration = 2, AttacksToEvade = 1 };
        AgilityStatus agility = new AgilityStatus() { Duration = 2, BonusMovement = 1, LinkedTo = evasive };

        // Move faster and evade an attack...
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode() { Status = evasive },
            new ApplyStatusNode() { Status = agility }
        };
    }
}
