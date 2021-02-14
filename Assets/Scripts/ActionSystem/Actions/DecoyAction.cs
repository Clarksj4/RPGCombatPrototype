using System.Collections.Generic;

public class DecoyAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 2 }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction(),
            new CellContentRestriction() { Content = TargetableCellContent.Ally }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint()
        };

        // Heal self at start of action
        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 2 },
            new HealNode() { Amount = 20 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new SwapNode()
        };
    }
}
