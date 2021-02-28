using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class StatusSet : MonoBehaviour
{
    /// <summary>
    /// Occurs when a status is applied to this pawn.
    /// </summary>
    public UnityEvent<PawnStatus> OnApplied;
    /// <summary>
    /// Occurs when a status is removed from this pawn.
    /// </summary>
    public UnityEvent<PawnStatus> OnExpired;

    // Fields
    private List<PawnStatus> statuses = new List<PawnStatus>();

    private Pawn pawn;

    private void Awake()
    {
        pawn = GetComponent<Pawn>();
    }

    /// <summary>
    /// Adds the given status to the attached pawn.
    /// </summary>
    public void Add(PawnStatus status)
    {
        bool collated = statuses.Any(s => s.Collate(status));
        if (!collated)
        {
            statuses.Add(status);
            status.Apply(pawn);
            OnApplied?.Invoke(status);
        }
    }

    /// <summary>
    /// Removes the given status from this pawn.
    /// </summary>
    public void Remove(PawnStatus status)
    {
        statuses.Remove(status);
        OnExpired?.Invoke(status);
    }

    /// <summary>
    /// Checks if this pawn is currently affected by
    /// a status of the given type.
    /// </summary>
    public bool Contains<T>() where T : PawnStatus
    {
        return statuses.Any(s => s is T);
    }

    /// <summary>
    /// Checks if this pawn is currently affected by
    /// a status with the given name.
    /// </summary>
    public bool Contains(string status)
    {
        return statuses.Any(s => s.GetType().Name.Contains(status));
    }
}
