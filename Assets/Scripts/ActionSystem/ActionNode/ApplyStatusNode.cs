using SimpleBehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStatusNode : ActionNode
{
    [Tooltip("The status to apply to the targeted cell.")]
    public PawnStatus Status;

    public override bool Do(BehaviourTreeState state)
    {
        // Get relevant data from state.
        Pawn actor = state.Get<Pawn>("Actor");
        Pawn targetPawn = state.Get<Cell>("Cell")
                              ?.GetContent<Pawn>();

        // Make a duplicate of the status and apply that instead so 
        // that the above status is not passed to ALL the things 
        // that it is applied to.
        PawnStatus duplicate = Status.Duplicate();

        // Apply status
        duplicate.Applicator = actor;
        targetPawn?.Statuses.Add(duplicate);

        // Update state with the status that was applied
        var statuses = state.Get<List<PawnStatus>>("Statuses");
        if (statuses == null)
            state["Statuses"] = new List<PawnStatus>();
        statuses = state.Get<List<PawnStatus>>("Statuses");
        statuses.Add(duplicate);

        // Counts as success if a status was applied.
        return targetPawn != null;
    }
}
