
using SimpleBehaviourTree;
using Sirenix.OdinInspector;
using UnityEngine;

public class SummonNode : ActionNode
{
    [Tooltip("The thing to summon.")]
    public PawnData Pawn;
    [HideIf("@this.Pawn != null")]
    [Tooltip("The name of the thing to summon.")]
    public string Name;
    [Tooltip("The duration the summoned thing will persist for.")]
    public int Duration;

    public override bool Do(BehaviourTreeState state)
    {
        Cell target = state.Get<Cell>("Cell");
        Pawn occupant = target.GetContent<Pawn>();
        bool occupied = occupant != null;

        // Spawn pawn if the cell is unoccupied.
        if (!occupied)
        {
            // Spawn Pawn
            if (Pawn == null)
                SummonManager.Instance.Spawn(Name, target, Duration);

            // Spawn Pawn by name
            else
                SummonManager.Instance.Spawn(Pawn, target, Duration);
        }
        
        // Success if the target cell wasn't occupied
        return !occupied;
    }
}
