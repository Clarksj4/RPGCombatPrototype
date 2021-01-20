using UnityEngine;
using System.Collections;

public class SleepStatus : PawnStatus
{
    public SleepStatus(int duration)
        : base(duration) { /* Nothing! */ }

    protected override void OnApplication()
    {
        Pawn.OnHealthChanged += Pawn_OnHealthChanged;

        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Sleeping = true;
        }

        else Expire();
    }

    protected override void OnExpired()
    {
        Pawn.OnHealthChanged -= Pawn_OnHealthChanged;

        if (Pawn is Actor)
        {
            Actor actor = Pawn as Actor;
            actor.Sleeping = false;
        }
    }

    private void Pawn_OnHealthChanged(int delta)
    {
        if (delta < 0)
            Expire();
    }
}
