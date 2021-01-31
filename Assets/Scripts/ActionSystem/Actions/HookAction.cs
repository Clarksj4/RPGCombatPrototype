using System.Collections.Generic;

public class HookAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Forced | ActionTag.Damage;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction(),
            new AdjcentCellContentRestriction() { Content = TargetableCellContent.Empty, Directions = RelativeDirection.Towards },
            new CellContentRestriction() { Content = TargetableCellContent.Enemy }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new PushNode() { RelativeDirection = RelativeDirection.Towards, Distance = 1},
            new DoDamageNode() { BaseDamage = 15 }
        };
    }
}
