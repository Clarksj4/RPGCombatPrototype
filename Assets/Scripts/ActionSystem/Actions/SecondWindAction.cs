using System.Collections;
using System.Collections.Generic;

public class SecondWindAction : BattleAction
{
    public override ActionTag Tags { get { return ActionTag.Heal; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Self; } }

    public SecondWindAction()
    {
        actionSequence = new List<ActionNode>()
        {
            new HealNode(this)
        };
    }
}
