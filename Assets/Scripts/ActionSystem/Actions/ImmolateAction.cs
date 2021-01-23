using System.Collections.Generic;

public class ImmolateAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new HealthRestriction(this, 10 + 1)
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction(this, TargetableFormation.Other),
            new LinearCellsRestriction(this),
            new ExposedCellsRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Enemy)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRange(this, 0, 1)
        };

        beginningActions = new List<ActionNode>()
        {
            new DoDamageNode() 
            { 
                Actor = Actor, 
                BaseDamage = 10, 
                Amplifyable = false, 
                Defendable = false, 
                Target = Actor.Cell 
            }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 5 },
            new ApplyStatusNode() { Actor = Actor, Status = new VulnerabilityStatus() { Duration = 1 } }
        };
    }
}
