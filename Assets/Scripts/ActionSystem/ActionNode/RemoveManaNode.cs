using UnityEngine;

public class RemoveManaNode : ActionNode
{
    /// <summary>
    /// Gets or sets the amount of damage this action will do.
    /// </summary>
    public int Amount;

    public override bool Do(Pawn actor, Cell target)
    {
        Pawn defender = target.GetContent<Pawn>();
        if (defender != null)
            defender.Stats["Mana"]?.Decrement(Amount);

        return true;
    }
}
