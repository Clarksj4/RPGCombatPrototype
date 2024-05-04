using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.TurnBased
{
    public class TurnManagerComponent : MonoBehaviour
    {
        /// <summary>
        /// Occurs when a new round is starting. A round is
        /// a full cycle of all actors in the turn order.
        /// </summary>
        public UnityEvent OnRoundStart = null;
        /// <summary>
        /// Occurs when a round is ending. A dround is a
        /// full cycle of all actors in the turn order.
        /// </summary>
        public UnityEvent OnRoundEnd = null;
        /// <summary>
        /// Occurs when an actor's turn is starting.
        /// </summary>
        public UnityEvent<ITurnBased> OnTurnStart = null;
        /// <summary>
        /// Occurs when an actor's turn is ending.
        /// </summary>
        public UnityEvent<ITurnBased> OnTurnEnd = null;
        /// <summary>
        /// Gets the order in which actors will have a turn
        /// during each round.
        /// </summary>
        public IReadOnlyCollection<ITurnBased> OrderOfActors => turnManager.OrderOfActors;
        /// <summary>
        /// The index of the current round.
        /// </summary>
        public int RoundCount => turnManager.RoundCount;

        private TurnManager turnManager = new TurnManager();

        /// <summary>
        /// Adds the given actor to the turn order.
        /// </summary>
        public void Add(ITurnBased actor)
        {
            turnManager.Add(actor);
        }

        /// <summary>
        /// Removes the given actor from the turn order.
        /// </summary>
        public bool Remove(ITurnBased actor)
        {
            return turnManager.Remove(actor);
        }

        public void UpdatePosition(ITurnBased actor)
        {
            turnManager.UpdatePosition(actor);
        }

        public void RequestTurnEnd()
        {
            turnManager.RequestTurnEnd();
        }
    }
}