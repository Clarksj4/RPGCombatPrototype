using System.Collections.Generic;

public class AttackAction : BattleAction
{
    public override int Range { get { return Actor.Reach; } }
    public override ActionTag Tags { get { return ActionTag.Damage; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Enemy; } }

    public AttackAction()
    {
        actionSequence = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this)
        };
    }
}
