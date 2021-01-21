using UnityEngine;
using System.Collections;

public abstract class PawnStatus
{
    public Pawn Pawn { get; set; }
    public int Duration { get; protected set; }

    public PawnStatus(int duration)
    {
        Duration = duration;
    }

    public void Apply()
    {
        TurnManager.Instance.OnTurnStart += HandleOnTurnBegin;
        TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;

        OnApplication();
    }

    public virtual bool Collate(PawnStatus other) { return false; }

    protected virtual void OnApplication() { /* Nothing! */ }

    protected virtual void DoEffect() {  /* Nothing! */ }

    protected virtual void OnExpired() { /* Nothing! */ }

    protected void Expire()
    {
        TurnManager.Instance.OnTurnStart -= HandleOnTurnBegin;
        TurnManager.Instance.OnTurnEnd -= HandleOnTurnEnd;

        OnExpired();
        Pawn.RemoveStatus(this);
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
