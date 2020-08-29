using System;
using System.Collections.Generic;
using System.Linq;

public class PrioritizedStartManager : Singleton<PrioritizedStartManager>
{
    private Dictionary<int, IStartable> startables = new Dictionary<int, IStartable>();
    private HashSet<IStartable> initializedStartables;
    private Action onComplete;

    /// <summary>
    /// Registers a startable to be intialized in priority order 
    /// at a later stage.
    /// </summary>
    public void RegisterWithPriority(IStartable startable, int priority)
    {
        // Can't add duplicate priorities.
        if (startables.ContainsKey(priority))
            throw new ArgumentException($"Startable: {startable} cannot be registered because {startables[priority]} already exists at priority: {priority}");

        startables.Add(priority, startable);
    }

    /// <summary>
    /// Initializes all the registered startables in priority
    /// order.
    /// </summary>
    public void InitializeAll(Action onComplete)
    {
        this.onComplete = onComplete;
        initializedStartables = new HashSet<IStartable>();
        IEnumerable<IStartable> orderedStartabkes = startables.OrderBy(kvp => kvp.Key)
                                                              .Select(kvp => kvp.Value);
        foreach (IStartable startable in orderedStartabkes)
        {
            bool complete = startable.Initialize();
            if (complete)
                initializedStartables.Add(startable);
        }

        CheckForComplete();
    }

    /// <summary>
    /// Marks a startable as having finished its initialization
    /// process.
    /// </summary>
    public void MarkInitializationComplete(IStartable startable)
    {
        initializedStartables.Add(startable);

        CheckForComplete();
    }

    private void CheckForComplete()
    {
        if (initializedStartables.Count == startables.Count)
        {
            onComplete?.Invoke();
            onComplete = null;
        }
    }
}
