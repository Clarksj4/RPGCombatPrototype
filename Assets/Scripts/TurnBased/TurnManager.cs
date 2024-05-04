using System;
using System.Collections.Generic;

public class TurnManager : Singleton<TurnManager>
{
    /// <summary>
    /// Occurs when a new round is starting. A round is
    /// a full cycle of all actors in the turn order.
    /// </summary>
    public event Action OnRoundStart;
    /// <summary>
    /// Occurs when a round is ending. A dround is a
    /// full cycle of all actors in the turn order.
    /// </summary>
    public event Action OnRoundEnd;
    /// <summary>
    /// Occurs when an actor's turn is starting.
    /// </summary>
    public event Action<ITurnBased> OnTurnStart;
    /// <summary>
    /// Occurs when an actor's turn is ending.
    /// </summary>
    public event Action<ITurnBased> OnTurnEnd;

    /// <summary>
    /// Gets the order in which actors will have a turn
    /// during each round.
    /// </summary>
    public IReadOnlyCollection<ITurnBased> OrderOfActors { get { return turnOrder; } }
    /// <summary>
    /// The index of the current round.
    /// </summary>
    public int RoundCount { get; private set; }

    private bool turnEndInProgress;
    private bool turnEndRequested;
    private bool roundStarted;
    private ITurnOrder turnOrder = new TurnOrder();

    /// <summary>
    /// Adds the given actor to the turn order.
    /// </summary>
    public void Add(ITurnBased actor)
    {
        turnOrder.Add(actor);
    }

    /// <summary>
    /// Removes the given actor from the turn order.
    /// </summary>
    public bool Remove(ITurnBased actor)
    {
        return turnOrder.Remove(actor);
    }

    /// <summary>
    /// Updates the given actors order in the turn.
    /// </summary>
    public void UpdatePosition(ITurnBased actor)
    {
        turnOrder.UpdatePosition(actor);
    }

    /// <summary>
    /// Requests that the current turn ends - will finish
    /// any in progress sequences before starting a new turn.
    /// </summary>
    public void RequestTurnEnd()
    {
        // If a turn is already ending - wait for it to finish
        // before ending the next one (so that ALL listeners
        // get notified in order)
        if (turnEndInProgress)
            turnEndRequested = true;

        else
            Next();
    }

    /// <summary>
    /// Proceeds to the next thing in the turn order if
    /// there is one.
    /// </summary>
    private void Next()
    {
        turnEndRequested = false;
        turnEndInProgress = true;

        // Notify that the current actor's turn is ending
        if (turnOrder.Current != null)
        {
            turnOrder.Current.OnTurnEnd();
            OnTurnEnd?.Invoke(turnOrder.Current);
        }

        // If the round hasn't started notify that a new one
        // is starting
        if (!roundStarted)
        {
            RoundCount++;
            OnRoundStart?.Invoke();
            roundStarted = true;
        }

        // Go to next thing in the turn order
        bool anyMore = turnOrder.MoveNext();

        // If there's nothing left in the order then the round has ended
        if (!anyMore)
        {
            roundStarted = false;
            turnOrder.Reset();
            OnRoundEnd?.Invoke();
        }
            
        // Otherwise, let the new thing know its turn is
        // starting
        else
        {
            // Notify the turn based thing FIRST - then other listeners.
            turnOrder.Current.OnTurnStart();
            OnTurnStart?.Invoke(turnOrder.Current);
        }

        turnEndInProgress = false;

        if (turnEndRequested)
            Next();
    }
}