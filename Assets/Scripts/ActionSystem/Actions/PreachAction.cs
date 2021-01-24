using System.Collections.Generic;

public class PreachAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new ExposedCellsRestriction() { Actor = Actor },
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
            new DoDamageNode() { Actor = Actor, BaseDamage = 5 },
            new HasStatusNode<WeakenedStatus>() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 10 }
        };
    }
}
