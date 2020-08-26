using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public IEnumerable<ITurnBased> OrderOfActors { get { return turnOrder; } }

    private bool roundStarted;
    private TurnOrder turnOrder = new TurnOrder();

    /// <summary>
    /// Proceeds to the next thing in the turn order if
    /// there is one.
    /// </summary>
    public bool Next()
    {
        // Notify that the current actor's turn is ending
        if (turnOrder.Current != null)
            OnTurnEnd?.Invoke(turnOrder.Current);

        // If the round hasn't started notify that a new one
        // is starting
        if (!roundStarted)
        {
            OnRoundStart?.Invoke();
            roundStarted = true;
        }

        // Go to next thing in the turn order
        bool anyMore = turnOrder.MoveNext();

        // If there's nothing left in the order then the round has ended
        if (!anyMore)
        {
            OnRoundEnd?.Invoke();
            roundStarted = false;
        }
            
        // Otherwise, let the new thing know its turn is
        // starting
        else
            OnTurnStart?.Invoke(turnOrder.Current);
        
        return anyMore;
    }
}