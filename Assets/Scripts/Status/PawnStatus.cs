using UnityEngine;
using System.Collections;
using System;

[Serializable]
public abstract class PawnStatus
{
    // TODO: maybe need to remove ref to Pawn for the sake of serialization?
    // TODO: otherwise, on deserialization, would need to get the reference again.
    // TODO: its already awkward to have to wire up listeners again on deserialization...

    /// <summary>
    /// Occurs when this status expires.
    /// </summary>
    public event Action<PawnStatus> OnStatusExpired;
    /// <summary>
    /// Gets the pawn this status is applied to.
    /// </summary>
    public Pawn Pawn { get; set; }
    /// <summary>
    /// Gets or sets the remaining duration that this
    /// status will afflicted the targeted pawn.
    /// </summary>
    public int Duration 
    { 
        get { return duration; }
        set { duration = value; }
    }
    [SerializeField]
    private int duration;
    /// <summary>
    /// Creates a new status that lasts for the given
    /// number of turns.
    /// </summary>
    public PawnStatus(int duration)
    {
        Duration = duration;
    }
    /// <summary>
    /// Applies this status.
    /// </summary>
    public void Apply()
    {
        TurnManager.Instance.OnTurnStart += HandleOnTurnBegin;
        TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;

        OnApplication();
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
    protected void Expire()
    {
        TurnManager.Instance.OnTurnStart -= HandleOnTurnBegin;
        TurnManager.Instance.OnTurnEnd -= HandleOnTurnEnd;

        OnExpired();
        Pawn.RemoveStatus(this);
        OnStatusExpired?.Invoke(this);
    }

    private void HandleOnTurnBegin(ITurnBased ent)
    {
        if (ent == (ITurnBased)Pawn)
        {
            DoEffect();

            // Duration reduced at the START of turn
            // so that the duration only decreased if
            // the status actually did something.
            Duration -= 1;
        }
    }

    private void HandleOnTurnEnd(ITurnBased ent)
    {
        // If expired...
        if (ent == (ITurnBased)Pawn && 
            Duration == 0)
            Expire();
    }
}
