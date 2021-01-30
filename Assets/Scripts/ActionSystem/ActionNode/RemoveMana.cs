using UnityEngine;

public class RemoveMana : ActionNode
{
    /// <summary>
    /// Gets or sets the amount of damage this action will do.
    /// </summary>
    public int Amount { get; set; }

    public override bool Do()
    {
        Pawn defender = Target.GetContent<Pawn>();
        if (defender != null)
            defender.SetMana(defender.Mana - Amount);

        return true;
    }
}
