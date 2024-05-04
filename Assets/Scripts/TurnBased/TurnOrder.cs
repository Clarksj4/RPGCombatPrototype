using System.Collections;
using System.Collections.Generic;


public class TurnOrder : ITurnOrder
{
    /// <summary>
    /// The current actor whose turn it is.
    /// </summary>
    public ITurnBased Current { get { return current != null ? current.Value : null; } }

    /// <summary>
    /// Gets the number of actors in this turn order.
    /// </summary>
    public int Count => turnOrder.Count;

    private LinkedListNode<ITurnBased> current;
    private LinkedList<ITurnBased> turnOrder = new LinkedList<ITurnBased>();
    private LinkedListNode<ITurnBased> toBeRemoved;

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
        if (current != null &&
            current.Value == actor)
        {
            toBeRemoved = current;
            return true;
        }
            
        else
            return turnOrder.Remove(actor);
    }

    private bool IsRemoved(ITurnBased actor)
    {
        return toBeRemoved != null &&
                toBeRemoved.Value != null &&
                toBeRemoved.Value == actor;
    }

    /// <summary>
    /// Updates the actors position in the turn order.
    /// </summary>
    public void UpdatePosition(ITurnBased actor)
    {
        if (!IsRemoved(actor))
        {
            Remove(actor);
            Add(actor);
        }
    }

    /// <summary>
    /// Gets the next actor in the round.
    /// </summary>
    public bool MoveNext()
    {
        // At the end of the order, loop back to the start
        if (current == null)
            current = turnOrder.First;
        
        // Go to next in order
        else
            current = current.Next;

        // Clean up anything waiting to be destroyed.
        if (toBeRemoved != null)
        {
            turnOrder.Remove(toBeRemoved);
            toBeRemoved = null;
        }
        
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