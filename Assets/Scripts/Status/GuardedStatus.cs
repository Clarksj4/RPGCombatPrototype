using UnityEngine;
using System.Collections;
using System;

public class GuardedStatus : PawnStatus
{
    public Pawn Protector { get; private set; }

    public GuardedStatus(int duration, Pawn protector)
        : base(duration) 
    {
        Protector = protector;
    }

    protected override void OnApplication()
    {
        Pawn.OnHealthChanged += HandleOnHealthChanged;
    }

    protected override void OnExpired()
    {
        Pawn.OnHealthChanged -= HandleOnHealthChanged;
    }

    private void HandleOnHealthChanged(int delta)
    {
        if (delta < 0)
        {
            Protector.TakeDamage(-delta, false);
            Pawn.GainHealth(-delta);
        }
    }
}
