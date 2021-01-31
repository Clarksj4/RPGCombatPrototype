using System.Collections.Generic;

public class PreachAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new ExposedCellsRestriction(),
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
            new DoDamageNode() { BaseDamage = 10 },
            new HasStatusNode<WeakenedStatus>(),
            new DoDamageNode() { BaseDamage = 10 }
        };
    }
}
