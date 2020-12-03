using UnityEngine;
using System.Collections;


public class SecondWindAction : BattleAction
{
    public override ActionTag Tags { get { return ActionTag.Heal; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Self; } }

    public override IEnumerator Do()
    {
        Pawn pawn = TargetFormation.GetPawnAtCoordinate(TargetPosition);
        Actor actor = pawn as Actor;

        actor.Health += 10;
        return null;
    }
}
