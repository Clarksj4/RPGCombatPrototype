
using Sirenix.OdinInspector;
using UnityEngine;

public class SummonNode : ActionNode
{
    [Tooltip("The thing to summon.")]
    public PawnStats Pawn;
    [HideIf("@this.Pawn != null")]
    [Tooltip("The name of the thing to summon.")]
    public string Name;
    [Tooltip("The duration the summoned thing will persist for.")]
    public int Duration;

    public override bool Do(Pawn actor, Cell target)
    {
        // Spawn Pawn
        if (Pawn == null)
            SummonManager.Instance.Spawn(Name, target, Duration);

        // Spawn Pawn by name
        else
            SummonManager.Instance.Spawn(Pawn, target, Duration);
        
        return true;
    }
}
