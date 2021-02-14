using System.Collections.Generic;

public class HibernateAction : BattleAction
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
            new AffectedPoint()
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 },
            new HealNode() { Amount = 20 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode() { Status = new LinkedStatuses().Add(new SleepStatus() { Duration = 4 })
                                                                 .Add(new RenewStatus() { Duration = 4, HealPerTurn = 15 }) }
        };
    }
}
