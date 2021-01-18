using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : IEnumerable<ITurnBased>
{
    // TODO: fuck this shit up! so that things can be at the same priority order
    // and removing one of them doesn't mess things up.
    // ALSO: don't need to maintain func to give actors multiple turns if their
    // priority is waaay higher that others

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