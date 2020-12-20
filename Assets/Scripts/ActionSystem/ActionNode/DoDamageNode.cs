using System.Linq;
using UnityEngine;

public class DoDamageNode : ActionNode
{
    public DoDamageNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell cell)
    {
        // Are there any targets for this action?
        bool any = cell.Contents.Any(c => c is IDefender);

        foreach (IGridBased target in cell.Contents)
        {
            if (target is IDefender)
            {
                // Do damage to all targets in cell
                IDefender defender = target as IDefender;

                // Damage can't be below 0
                int damage = (int)Mathf.Max(0, action.Actor.Attack - defender.Defense);
                Debug.Log($"Defender takes {damage} damage.");
                defender.Health -= damage;
            }
        }

        return any;
    }
}
