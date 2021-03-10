using UnityEngine;
using System.Collections.Generic;

public class LinkedStatuses : PawnStatus
{
    [Tooltip("The statuses that are linked together.")]
    public List<PawnStatus> linkedStatuses = new List<PawnStatus>();

    public override PawnStatus Duplicate()
    {
        LinkedStatuses duplicate = base.Duplicate() as LinkedStatuses;
        foreach (PawnStatus status in linkedStatuses)
            duplicate.linkedStatuses.Add(status.Duplicate());
        return duplicate;
    }

    /// <summary>
    /// Convenience method for adding statuses that are linked.
    /// </summary>
    public LinkedStatuses Add(PawnStatus status)
    {
        linkedStatuses.Add(status);
        return this;
    }

    protected override void OnApplication()
    {
        base.OnApplication();
        foreach (PawnStatus status in linkedStatuses)
            Pawn.Statuses.Add(status);

        Pawn.Statuses.OnExpired.AddListener(HandleOnStatusExpired);
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        foreach (PawnStatus status in linkedStatuses)
            status.Expire();
    }

    private void HandleOnStatusExpired(PawnStatus status)
    {
        if (linkedStatuses.Contains(status))
            Expire();
    }
}
