using Sirenix.Serialization;
using System;

public class HasStatusNode : ActionNode
{
    public string StatusName;

    public override bool Do(Pawn actor, Cell target)
    {
        Pawn defender = target.GetContent<Pawn>();
        if (defender != null)
            return defender.HasStatus(StatusName);

        // Fudge it if there is no defender
        return true;
    }
}
