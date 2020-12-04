using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            new PushNode(this)
        };

        targetRestrictions.Add(new EmptyAdjacentRestriction(this));
    }
}
