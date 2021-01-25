using System.Collections.Generic;

public class DecoyAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction() { Actor = Actor },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Ally }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Heal self at start of action
        beginningActions = new List<ActionNode>()
        {
            new HealNode() { Actor = Actor, Target = OriginCell, Amount = 20 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new SwapNode() { Actor = Actor }
        };
    }
}
