using System.Collections.Generic;

public class FireboltAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction()  { Actor = Actor },
            new FormationRestriction()    { Actor = Actor, Formations = TargetableFormation.Other },
            new CellContentRestriction()  { Actor = Actor, Content = TargetableCellContent.Enemy },
            new ExposedCellsRestriction() { Actor = Actor }
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
            new DoDamageNode() { Actor = Actor, BaseDamage = 15 }
        };
    }
}
