using System.Collections.Generic;

public class PenanceAction : BattleAction
{
    public PenanceAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Not usable at the front of the grid
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 1, 2)
        };

        // Self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Self)
        };

        // Just the one target actually...
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Heal and stun self
        targetedActions = new List<ActionNode>()
        {
            new HealNode(this) { Amount = 20 },
            new ApplyStatusNode(this) { Status = new StunnedStatus() { Duration = 1 } }
        };
    }
}
