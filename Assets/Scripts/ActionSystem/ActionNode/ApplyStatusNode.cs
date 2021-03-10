using SimpleBehaviourTree;
using UnityEngine;

public class ApplyStatusNode : ActionNode
{
    [Tooltip("The status to apply to the targeted cell.")]
    public PawnStatus Status;

    public override bool Do(BehaviourTreeState state)
    {
        // Get relevant data from state.
        Pawn actor = state.Get<Pawn>("Actor");
        Cell targetCell = state.Get<Cell>("Cell");
        Pawn targetPawn = targetCell.GetContent<Pawn>();

        // Apply status
        Status.Applicator = actor;
        targetPawn?.Statuses.Add(Status);

        // Counts as success if a status was applied.
        return targetPawn != null;
    }
}
