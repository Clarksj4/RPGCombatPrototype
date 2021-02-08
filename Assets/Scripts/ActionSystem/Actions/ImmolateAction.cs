using System.Collections.Generic;

public class ImmolateAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 },
            new HealthRestriction() { Amount = 10 }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FileCellsRestriction(),
            new ExposedCellsRestriction(),
            new CellContentRestriction() { Content = TargetableCellContent.Enemy }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRange(this, 0, 1)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 },
            new DoDamageNode() { BaseDamage = 15, Amplifyable = false, Defendable = false }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new DoDamageNode() { BaseDamage = 15 },
            new ApplyStatusNode() { Status = new WeakenedStatus() { Duration = 1 } }
        };
    }
}
