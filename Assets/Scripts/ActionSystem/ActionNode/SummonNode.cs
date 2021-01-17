using UnityEngine;
using System.Collections;

public class SummonNode : ActionNode
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public float Priority { get; set; }

    public SummonNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool Do()
    {
        SummonManager.Instance.Spawn(Name, Target, Priority, Duration);
        return true;
    }
}
