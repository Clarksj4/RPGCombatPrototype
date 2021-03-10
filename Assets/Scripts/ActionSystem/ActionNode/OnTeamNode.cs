using Sirenix.Serialization;
using System;

public class OnTeamNode : ActionNode
{
    public TeamFlags Allegiance = TeamFlags.None;

    public override bool Do(Pawn actor, Cell target)
    {
        Pawn defender = target.GetContent<Pawn>();
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

            return false;
        }
         
        return false;
    }
}
