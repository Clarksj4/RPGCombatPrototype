using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinkedStatuses : PawnStatus
{
    public List<PawnStatus> linkedStatuses = new List<PawnStatus>();

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
            Pawn.AddStatus(status);

        Pawn.OnStatusExpired += HandleOnStatusExpired;
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
