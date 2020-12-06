using System.Collections.Generic;

public class MoveAction : BattleAction
{
    public override int Range { get { return Actor.Movement; } }
    public override ActionTag Tags { get { return ActionTag.Movement; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Empty; } }

    public MoveAction()
    {
        actionSequence = new List<ActionNode>()
        {
            new MoveActorNode(this)
        };
    }
}