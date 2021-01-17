using System.Collections.Generic;

public class DecoyAction : BattleAction
{
    public DecoyAction(Actor actor)
        : base(actor) 
    {
        // Misc information about the ability
        Tags = ActionTag.Movement;
    }

    protected override void Setup()
    {
        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Ally)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Heal self at start of action
        beginningActions = new List<ActionNode>()
        {
            new HealNode(this) { Target = OriginCell, Amount = 10 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new SwapNode(this)
        };
    }
}
