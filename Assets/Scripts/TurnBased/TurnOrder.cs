using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : IEnumerable<ITurnBased>
{
    /// <summary>
    /// The current actor whose turn it is.
    /// </summary>
    public ITurnBased Current { get { return current != null ? current.Value : null; } }
    
    private LinkedListNode<ITurnBased> current;
    private LinkedList<ITurnBased> turnOrder = new LinkedList<ITurnBased>();

    /// <summary>
    /// Adds the given actor to the current round order.
    /// </summary>
    public void Add(ITurnBased actor)
    {
        // List is oriented: High -> Low priority

        // Traverse list until we find an actor with greater
        // priority than this actor-
        LinkedListNode<ITurnBased> walker = turnOrder.First;
        while (walker != null &&
                walker.Value.Priority >= actor.Priority)
            walker = walker.Next;

        // Add actor to end of list if there's none with lower priority
        if (walker == null)
            turnOrder.AddLast(actor);

        // Add after the last actor with a higher priority.
        else 
            turnOrder.AddAfter(walker, actor);
    }

    /// <summary>
    /// Removes the given actor from the current round order.
    /// </summary>
    public bool Remove(ITurnBased actor)
    {
        // If its their turn - don't remove them until the end of the round
        // OR maybe the local ref handles this already?

        return turnOrder.Remove(actor);
    }

    /// <summary>
    /// Updates the actors position in the turn order.
    /// </summary>
    public void UpdatePosition(ITurnBased actor)
    {
        Remove(actor);
        Add(actor);
    }

    /// <summary>
    /// Gets the next actor in the round.
    /// </summary>
    public bool MoveNext()
    {
        if (current == null)
            current = turnOrder.First;
        
        else
            current = current.Next;
        
        return current != null;
    }

    /// <summary>
    /// Reset this round.
    /// </summary>
    public void Reset()
    {
        current = null;
    }

    public IEnumerator<ITurnBased> GetEnumerator()
    {
        return turnOrder.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return turnOrder.GetEnumerator();
    }
}