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
        if (ent == Pawn)
            DoEffect();
    }

    private void HandleOnTurnEnd(ITurnBased ent)
    {
        if (ent == Pawn)
        {
            Duration -= 1;

            // Expired!
            if (Duration == 0)
                Expire();
        }
    }
}
