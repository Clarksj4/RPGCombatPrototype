using System;
using UnityEngine;

[Serializable]
public class EvasiveStatus : PawnStatus
{
    /// <summary>
    /// Gets the number of attacks the afflicted pawn
    /// will evade while affected by this status.
    /// </summary>
    public int AttacksToEvade;

    public override bool Collate(PawnStatus other)
    {
        // Extend duration and combined the number of attacks to evade
        if (other is EvasiveStatus)
        {
            Duration = Mathf.Max(Duration, other.Duration);
            AttacksToEvade += ((EvasiveStatus)other).AttacksToEvade;
            return true;
        }

        // Immobilized removes evasive
        else if (other is ImmobilizedStatus)
            Expire();

        return false;
    }

    protected override void OnApplication()
    {
        base.OnApplication();

        // Will evade until enough attacks are evaded
        Pawn.Stats["Evasive"].Value += 1;
        Pawn.OnAttacked += Pawn_OnAttacked;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Stats["Evasive"].Value -= 1;
    }

    private void Pawn_OnAttacked(bool obj)
    {
        // Reduce counter - expire if out of evades
        AttacksToEvade--;
        if (AttacksToEvade == 0)
            Expire();
    }
}
