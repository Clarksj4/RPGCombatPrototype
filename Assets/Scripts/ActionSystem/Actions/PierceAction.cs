using System.Collections.Generic;

public class PierceAction : BattleAction
{
    public PierceAction()
        : base()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FileCellsRestriction(this, () => { return Actor.Formation.GetFile(Actor.Cell.Coordinate); }),
            new ExposedCellsRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Enemy)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            // Hits the same file that the actor is in BUT actor is null
            // when the action is created.
            new AffectedFile(this, () => { return Actor.Formation.GetFile(Actor.Cell.Coordinate); })
        };

        // The effect upon those cells.
        actionSequence = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this)
        };

        // If one of the targets is not hit, then no subsequent targets are hit
        AffectedCellsIndependent = false;
    }
}
