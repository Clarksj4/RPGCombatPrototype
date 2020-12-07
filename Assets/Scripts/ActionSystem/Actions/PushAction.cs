using System.Collections.Generic;

public class PushAction : BattleAction
{
    /// <summary>
    /// The range of this push action.
    /// </summary>
    private const int RANGE = 1;

    public override int Range { get { return RANGE; } }
    public override ActionTag Tags { get { return ActionTag.Movement | ActionTag.Forced; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Ally | TargetableCellContent.Enemy; } }

    public PushAction()
    {
        actionSequence = new List<ActionNode>()
        {
            new PushNode(this, Range, RelativeDirection.Away)
        };

        targetRestrictions.Add(new EmptyAdjacentRestriction(this, RelativeDirection.Away));
    }
}
