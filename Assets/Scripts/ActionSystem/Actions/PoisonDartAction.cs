using System.Collections.Generic;

public class PoisonDartAction : BattleAction
{
    public PoisonDartAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Enemy),
            new ExposedCellsRestriction(this)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this, 5),
            new ApplyStatusNode(this) { Status = new PoisonStatus(2) }
        };
    }
}
