using System.Collections.Generic;

public class SecondWindAction : BattleAction
{
    public SecondWindAction()
    {
        // Misc information about the ability
        Tags = ActionTag.Heal;

        // Knowing what we can target
        targetableFormation = TargetableFormation.Self;
        targetableStrategy = new AnyCells(this);
        targetRestrictions = new List<TargetableCellRestriction>()
        {
            new CellContentRestriction(this,TargetableCellContent.Self)
        };
        targetedStrategy = new TargetedPoint(this);

        // Knowing what we do.
        actionSequence = new List<ActionNode>()
        {
            new HealNode(this)
        };
    }
}
