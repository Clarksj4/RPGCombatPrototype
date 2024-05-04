using System.Collections;
using System.Collections.Generic;

public interface ITurnOrder : IReadOnlyCollection<ITurnBased>
{
    /// <summary>
    /// The current actor whose turn it is.
    /// </summary>
    ITurnBased Current { get; }
    /// <summary>
    /// Adds the given actor to the current round order.
    /// </summary>
    void Add(ITurnBased actor);
    /// <summary>
    /// Removes the given actor from the current round order.
    /// </summary>
    bool Remove(ITurnBased actor);
    /// <summary>
    /// Updates the actors position in the turn order.
    /// </summary>
    void UpdatePosition(ITurnBased actor);
    /// <summary>
    /// Gets the next actor in the round.
    /// </summary>
    bool MoveNext();
    /// <summary>
    /// Reset this round.
    /// </summary>
    void Reset();
}