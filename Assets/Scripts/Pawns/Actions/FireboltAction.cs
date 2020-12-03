using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireboltAction : BattleAction
{

    public override ActionTag Tags { get { return ActionTag.Damage; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Enemy; } }
    public override TargetableFormation TargetableFormation { get { return TargetableFormation.Other; } }

    public FireboltAction()
        : base()
    {
        targetableStrategy = new LinearCells(this);
        actionSequence = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this)
        };
    }
}
