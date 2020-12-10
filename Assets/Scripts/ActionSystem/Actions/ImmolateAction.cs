using System.Collections.Generic;

public class ImmolateAction : BattleAction
{
    /// <summary>
    /// The area from the targeted cell that will be affected.
    /// </summary>
    private const int AREA = 1;

    public ImmolateAction()
        : base()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Knowing what we can target
        targetableFormation = TargetableFormation.Other;
        targetableStrategy = new SameRowExposedCells(this);
        targetRestrictions = new List<TargetableCellRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Enemy)
        };
        targetedStrategy = new TargetedArea(this, AREA);

        // Knowing what we do.
        actionSequence = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this)
        };
    }
}
