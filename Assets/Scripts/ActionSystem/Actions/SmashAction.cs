using System.Collections.Generic;

public class SmashAction : BattleAction
{
    public SmashAction()
        : base()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 0),
            new CellContentRestriction(this, TargetableCellContent.Enemy)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        actionSequence = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this),
            new PushNode(this, 1, RelativeDirection.Away)
        };
    }
}
