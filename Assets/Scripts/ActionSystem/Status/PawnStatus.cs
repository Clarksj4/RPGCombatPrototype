using UnityEngine;
using System.Collections;
using System;

[Serializable]
public abstract class PawnStatus
{
    /// <summary>
    /// Occurs when this status expires.
    /// </summary>
    public event Action<PawnStatus> OnStatusExpired;
    /// <summary>
    /// Gets the name of this pawn status.
    /// </summary>
    public string Name { get { return GetType().Name; } }
    /// <summary>
    /// Gets the pawn this status is applied to.
    /// </summary>
    public Pawn Pawn { get; private set; }
    /// <summary>
    /// Gets the actor who applied this status.
    /// </summary>
    public Pawn Applicator { get; set; }
    /// <summary>
    /// Gets or sets the remaining duration that this
    /// status will afflicted the targeted pawn.
    /// </summary>
    public int Duration;
    
    private bool expiring;
    
    /// <summary>
    /// Applies this status.
    /// </summary>
    public void Apply(Pawn pawn)
    {
        Pawn = pawn;

        pawn.OnTurnStarted.AddListener(HandleOnTurnStarted);
        pawn.OnTurnEnded.AddListener(HandleOnTurnEnded);

        OnApplication();
    }
    
    /// <summary>
    /// Creates a duplicate of this status.
    /// </summary>
    public virtual PawnStatus Duplicate()
    {
        PawnStatus duplicate = (PawnStatus)Activator.CreateInstance(GetType());
        duplicate.Duration = Duration;
        return duplicate;
    }

    /// <summary>
    /// Checks for and handle interactions between statuses.
    /// </summary>
    public virtual bool Collate(PawnStatus other) { return false; }
    /// <summary>
    /// Occurs when this status is applied to the target pawn.
    /// </summary>
    protected virtual void OnApplication() { /* Nothing! */ }
    /// <summary>
    /// Occurs at the beginning of each turn.
    /// </summary>
    protected virtual void DoEffect() {  /* Nothing! */ }
    /// <summary>
    /// Occurs when this status expires.
    /// </summary>
    protected virtual void OnExpired() { /* Nothing! */ }
    /// <summary>
    /// Removes this status from the afflicted pawn.
    /// </summary>
    public void Expire()
    {
        if (!expiring)
        {
            expiring = true;
            Pawn.OnTurnStarted.RemoveListener(HandleOnTurnStarted);
            Pawn.OnTurnEnded.RemoveListener(HandleOnTurnEnded);

            OnExpired();
            Pawn.Statuses.Remove(this);
            OnStatusExpired?.Invoke(this);
        }
    }

    private void HandleOnTurnStarted(Pawn pawn)
    {
        DoEffect();

        // Duration reduced at the START of turn
        // so that the duration only decreased if
        // the status actually did something.
        Duration -= 1;
    }

    private void HandleOnTurnEnded(Pawn pawn)
    {
        // If expired...
        if (Duration == 0)
            Expire();
    }
}
