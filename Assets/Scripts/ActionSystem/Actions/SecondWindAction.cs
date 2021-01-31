using System.Collections.Generic;

public class SecondWindAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Heal;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Self }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new HealNode() { Amount = 25 },
            new ApplyStatusNode() { Status = new PowerStatus() { Duration = 2 } },
            new ApplyStatusNode() { Status = new AgilityStatus() { Duration = 2, BonusMovement = 1 } }
        };
    }
}
