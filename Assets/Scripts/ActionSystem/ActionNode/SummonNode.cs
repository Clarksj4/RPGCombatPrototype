
using UnityEngine;

public class SummonNode : ActionNode
{
    [Tooltip("The name of the thing to summon.")]
    public string Name;
    [Tooltip("The duration the summoned thing will persist for.")]
    public int Duration;
    [Tooltip("The summoned thing's priority in the turn order.")]
    public float Priority;

    public override bool Do(Pawn actor, Cell target)
    {
        SummonManager.Instance.Spawn(Name, target, Priority, Duration);
        return true;
    }
}
