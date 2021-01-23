using System.Collections.Generic;

public class SecondWindAction : BattleAction
{
    public SecondWindAction(Actor actor) 
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
            new HealNode(this) { Amount = 10 },
            new ApplyStatusNode(this) { Status = new PowerStatus() { Duration = 2 } },
            new ApplyStatusNode(this) { Status = new AgilityStatus() { Duration = 2, BonusMovement = 1 } }
        };
    }
}
