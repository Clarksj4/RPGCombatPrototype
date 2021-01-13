using System.Collections.Generic;

public class PierceAction : BattleAction
{
    public PierceAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FileCellsRestriction(this, Actor.File),
            new ExposedCellsRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Enemy)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            // Hits the same file that the actor is in BUT actor is null
            // when the action is created.
            new AffectedFile(this, Actor.File)
        };

        // The effect upon those cells.
        targetActions = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this)
        };

        // If one of the targets is not hit, then no subsequent targets are hit
        AffectedCellsIndependent = false;
    }
}
