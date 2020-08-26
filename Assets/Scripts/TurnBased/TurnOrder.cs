using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : IEnumerable<ITurnBased>
{
    /// <summary>
    /// The granularity of each round - the higher this value the more
    /// opportunity actors will have to act in any single round.
    /// </summary>
    private const float MAX_PROGRESS = 100f;
    /// <summary>
    /// The current actor whose turn it is.
    /// </summary>
    public ITurnBased Current { get; private set; }
    /// <summary>
    /// The current round progress - PRIVATE because it doesn't really
    /// make sense to use it external to this class - mainly because the
    /// max progrses value can change so it's hard to message what this
    /// value actually represents in a game sense.
    /// </summary>
    private float progress;

    // Actual order in which actors will get to act
    private SortedList<float, ITurnBased> turnOrder;
    // Collection of actors
    private HashSet<ITurnBased> actors;

    /// <summary>
    /// Adds the given actor to the current round order.
    /// </summary>
    public bool Add(ITurnBased actor)
    {
        if (actors == null)
            actors = new HashSet<ITurnBased>();

        // Add actor unless its already in the order
        bool added = actors.Add(actor);

        // If the actor was added update order
        if (added)
            UpdateOrder();

        return added;
    }

    /// <summary>
    /// Removes the given actor from the current round order.
    /// </summary>
    public bool Remove(ITurnBased actor)
    {
        bool removed = actors.Remove(actor);

        // Update order to account for removed actor
        UpdateOrder();

        return removed;
    }

    /// <summary>
    /// Updates the order of actors for the current turn.
    /// </summary>
    public void UpdateOrder()
    {
        // Get prioritized list of actors for the entire round - may 
        // contain duplicates depending upon how many times an actor 
        // can act each turn
        turnOrder = CalculateTurnOrder();
    }

    /// <summary>
    /// Gets the next actor in the round.
    /// </summary>
    public bool MoveNext()
    {
        // Need to check if there are any actors left in the round
        if (GetNextActor(out var prioritizedActor))
        {
            // Get next actor
            Current = prioritizedActor.Value;

            // Turn progress is set to that actors priority
            progress = prioritizedActor.Key;
            return true;
        }

        else
        {
            // No more actors
            Current = null;
            return false;
        }
    }

    /// <summary>
    /// Reset this round.
    /// </summary>
    public void Reset()
    {
        Current = null;
        progress = 0;
    }

    private bool GetNextActor(out KeyValuePair<float, ITurnBased> prioritizedActor)
    {
        // Get the next actor that has priority greater than the
        // current juice level
        foreach (var kvp in turnOrder)
        {
            if (kvp.Key > progress)
            {
                prioritizedActor = kvp;
                return true;
            }
        }

        // No actors yet to act this turn
        return false;
    }

    private SortedList<float, ITurnBased> CalculateTurnOrder()
    {
        // Generate a list of actors that may include duplicates at different steps
        // depending upon how often each actor gets to act in a round
        SortedList<float, ITurnBased> actorOrder = new SortedList<float, ITurnBased>();
        foreach (ITurnBased actor in actors)
        {
            // How many turns per round does this actor get?
            int nOccurences = (int)Mathf.Max(1f, MAX_PROGRESS / actor.Priority);
            for (int i = 0; i < nOccurences; i++)
            {
                // Calculate the priority steps that this actor
                // gets to act at and add it to the list
                float nextPriority = actor.Priority + (actor.Priority * i);

                // OPTIMIZE: only walk the list once
                // Handle priority collisions by moving the next available slot
                while (actorOrder.ContainsKey(nextPriority))
                    nextPriority++;

                actorOrder.Add(nextPriority, actor);

                // TODO: maybe cull duplicates? so one character can't have
                // consecutive turns
            }
        }

        return actorOrder;
    }

    public IEnumerator<ITurnBased> GetEnumerator()
    {
        return turnOrder.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return turnOrder.Values.GetEnumerator();
    }
}