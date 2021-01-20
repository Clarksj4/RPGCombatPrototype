using System.Collections.Generic;

public class ImmolateAction : BattleAction
{
    /// <summary>
    /// The area from the targeted cell that will be affected.
    /// </summary>
    private const int AREA = 1;

    public ImmolateAction(Actor actor)
        : base(actor) { /* Nothing! */ }

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
            new AffectedRange(this, 0, AREA)
        };

        beginningActions = new List<ActionNode>()
        {
            new DoDamageNode(this, 10, false, false) { Target = Actor.Cell }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this, 5),
            new ApplyStatusNode(this) { Status = new VulnerabilityStatus(1) }
        };
    }
}
