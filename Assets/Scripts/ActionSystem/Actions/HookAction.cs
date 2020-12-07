using UnityEngine;
using System.Collections;

public class HookAction : BattleAction
{
    public override ActionTag Tags { get { return ActionTag.Damage; } }

    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Enemy; } }
}
