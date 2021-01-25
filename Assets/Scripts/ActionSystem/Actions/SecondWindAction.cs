using System.Collections.Generic;

public class SecondWindAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Heal;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Self }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new HealNode() { Actor = Actor, Amount = 25 },
            new ApplyStatusNode() { Actor = Actor, Status = new PowerStatus() { Duration = 2 } },
            new ApplyStatusNode() { Actor = Actor, Status = new AgilityStatus() { Duration = 2, BonusMovement = 1 } }
        };
    }
}
