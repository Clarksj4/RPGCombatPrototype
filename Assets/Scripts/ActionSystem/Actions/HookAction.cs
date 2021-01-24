using System.Collections.Generic;

public class HookAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Forced | ActionTag.Damage;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction() { Actor = Actor },
            new AdjcentCellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Empty, Directions = RelativeDirection.Towards },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Enemy }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new PushNode() { Actor = Actor, RelativeDirection = RelativeDirection.Towards, Distance = 1},
            new DoDamageNode() { Actor = Actor, BaseDamage = 15 }
        };
    }
}
