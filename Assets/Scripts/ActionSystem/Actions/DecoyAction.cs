using System.Collections.Generic;

public class DecoyAction : BattleAction
{
    public DecoyAction()
        : base()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement;

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

        // The effect upon those cells.
        actionSequence = new List<ActionNode>()
        {
            new SwapNode(this)
        };
    }
}
