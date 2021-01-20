using UnityEngine;
using System.Collections;

public class EvasiveStatus : PawnStatus
{
    public int AttacksToEvade { get; private set; }

    public EvasiveStatus(int duration, int attacksToEvade)
        : base(duration)
    {
        AttacksToEvade = attacksToEvade;
    }

    protected override void OnApplication()
    {
        base.OnApplication();
        Pawn.Evasive = true;
        Pawn.OnAttacked += Pawn_OnAttacked;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Evasive = false;
    }

    private void Pawn_OnAttacked(bool obj)
    {
        // Reduce counter - expire if out of evades
        AttacksToEvade--;
        if (AttacksToEvade == 0)
            Expire();
    }
}
