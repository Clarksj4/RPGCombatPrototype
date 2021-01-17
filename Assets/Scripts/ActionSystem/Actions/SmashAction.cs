using System.Collections.Generic;

public class SmashAction : BattleAction
{
    public SmashAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Actor must be in the front rank to use this ability
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 0)
        };

        // Can target enemies in the front rank
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 0),
            new CellContentRestriction(this, TargetableCellContent.Enemy)
        };

        // Affects only the targeted cell
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Swings, hits, and pushes away
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this),
            new PushNode(this) { RelativeDirection = RelativeDirection.Away, Distance = 1 }
        };
    }
}
