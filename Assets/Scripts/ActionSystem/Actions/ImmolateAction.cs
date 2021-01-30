using System.Collections.Generic;

public class ImmolateAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new HealthRestriction() { Actor = Actor, Amount = 10 }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction() { Actor = Actor, Formations = TargetableFormation.Other },
            new LinearCellsRestriction() { Actor = Actor },
            new ExposedCellsRestriction() { Actor = Actor },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Enemy }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRange(this, 0, 1)
        };

        selfActions = new List<ActionNode>()
        {
            new DoDamageNode() 
            { 
                Actor = Actor, 
                BaseDamage = 15, 
                Amplifyable = false, 
                Defendable = false, 
                Target = Actor.Cell 
            }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 15 },
            new ApplyStatusNode() { Actor = Actor, Status = new WeakenedStatus() { Duration = 1 } }
        };
    }
}
