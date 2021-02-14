using UnityEngine;

public class HealthRestriction : TargetingRestriction
{
    /// <summary>
    /// Gets or sets the amount of health the target
    /// must be over or equal to to be valid. A negative
    /// value means within that value from max health.
    /// </summary>
    public int Amount;

    public override bool IsTargetValid(Pawn actor, Cell cell)
    {
        Pawn pawn = cell.GetContent<Pawn>();
        if (pawn != null)
        {
            if (Amount >= 0)
                return pawn.Health > Amount;
            else
                return pawn.MaxHealth - pawn.Health > Mathf.Abs(Amount);
        }

        return true;
    }
}
