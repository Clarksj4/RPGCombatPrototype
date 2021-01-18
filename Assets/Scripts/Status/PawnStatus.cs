using UnityEngine;
using System.Collections;

public abstract class PawnStatus
{
    public Pawn Pawn { get; set; }
    public int Duration { get; set; }

    public PawnStatus(Pawn pawn, int duration)
    {
        Pawn = pawn;
        Duration = duration;
    }

    public void Apply()
    {
        TurnManager.Instance.OnTurnStart += HandleOnTurnBegin;

        OnApplication();
    }

    public virtual bool Collate(PawnStatus other) { return false; }

    protected virtual void OnApplication() { /* Nothing! */ }

    protected virtual void DoEffect() {  /* Nothing! */ }

    protected virtual void OnExpired() { /* Nothing! */ }

    protected void Expire()
    {
        OnExpired();
        Pawn.RemoveStatus(this);
    }

    private void HandleOnTurnBegin(ITurnBased ent)
    {
        if (ent == Pawn)
        {
            Duration -= 1;

            // Expired - remove without doing effect.
            if (Duration == 0)
                Expire();

            // Not expired - do the thing.
            else
                DoEffect();
        }
    }
}
