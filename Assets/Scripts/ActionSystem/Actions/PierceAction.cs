using System.Collections.Generic;

public class PierceAction : BattleAction
{
    public PierceAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Only usable from the front rank
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 0)
        };

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
            new AffectedFile(this, Actor.File)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this)
        };

        // If one of the targets is not hit, then no subsequent targets are hit
        AffectedCellsIndependent = false;
    }
}
