using UnityEngine;
using System.Linq;

public class StatusBar : MonoBehaviour
{
    [Tooltip("The frames that will hold icons for all the statuses applied to this pawn.")]
    public StatusFrame[] statusFrames;

    private Pawn pawn;

    private void Awake()
    {
        pawn = GetComponentInParent<Pawn>();
        pawn.OnStatusApplied += HandleOnStatusApplied;
        pawn.OnStatusExpired += HandleOnStatusExpired;
    }

    private void HandleOnStatusApplied(PawnStatus status)
    {
        StatusFrame existingFrame = statusFrames.FirstOrDefault(s => s.StatusName == status.GetType().Name);
        if (existingFrame != null)
            existingFrame.IncrementStatusCount();

        else
        {
            // Get first inactive frame (if there's still some)
            StatusFrame firstEmptyFrame = statusFrames.FirstOrDefault(s => !s.gameObject.activeSelf);
            if (firstEmptyFrame != null)
                firstEmptyFrame.SetStatus(status.GetType().Name);
        }
    }

    private void HandleOnStatusExpired(PawnStatus status)
    {
        StatusFrame existingFrame = statusFrames.FirstOrDefault(s => s.StatusName == status.GetType().Name);
        if (existingFrame != null)
            existingFrame.DecrementStatusCount();
    }
}
