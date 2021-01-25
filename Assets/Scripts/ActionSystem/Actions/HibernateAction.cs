using System.Collections.Generic;

public class HibernateAction : BattleAction
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

        beginningActions = new List<ActionNode>()
        {
            new HealNode() { Actor = Actor, Target = Actor.Cell, Amount = 20 }
        };

        SleepStatus sleep = new SleepStatus() { Duration = 4 };
        RenewStatus renew = new RenewStatus() { Duration = 4, HealPerTurn = 15, LinkedTo = sleep };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode() { Actor = Actor, Status = sleep },
            new ApplyStatusNode() { Actor = Actor, Status = renew }
        };
    }
}
