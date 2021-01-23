using System.Collections.Generic;

public class HibernateAction : BattleAction
{
    public HibernateAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Heal;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Self)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode(this) { Status = new SleepStatus() { Duration = 4 } },
            new ApplyStatusNode(this) { Status = new RenewStatus() { Duration = 4, HealPerTurn = 5} }
        };
    }
}
