using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Character", menuName = "Actor Stats")]
public class ActorStats : PawnStats
{
    [Header("Actions")]
    public List<string> Actions;

    public override void SetStats(Pawn pawn)
    {
        base.SetStats(pawn);

        if (pawn is Actor)
        {
            Actor actor = pawn as Actor;
            actor.Actions = Actions;
        }
    }
}
