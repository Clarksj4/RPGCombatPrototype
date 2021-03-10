using SimpleBehaviourTree;
using Sirenix.Serialization;
using System;
using UnityEngine;

public class OnTeamNode : ActionNode
{
    [Tooltip("The allegiance the target must have.")]
    public TeamFlags Allegiance = TeamFlags.None;

    public override bool Do(BehaviourTreeState state)
    {
        Pawn actor = state.Get<Pawn>("Actor");
        Pawn defender = state.Get<Cell>("Cell")
                            ?.GetContent<Pawn>();
        if (defender != null)
        {
            if (Allegiance.HasFlag(TeamFlags.Any) &&
                defender.Team != null) 
                return true;

            else if (Allegiance.HasFlag(TeamFlags.None) &&
                defender.Team == null) 
                return true;

            else if (Allegiance.HasFlag(TeamFlags.Same) &&
                defender.Team == actor.Team)
                return true;

            else if (Allegiance.HasFlag(TeamFlags.Other) &&
                defender.Team != actor.Team)
                return true;
        }
         
        return false;
    }
}
