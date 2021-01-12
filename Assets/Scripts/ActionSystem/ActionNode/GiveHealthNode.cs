using System.Linq;
using UnityEngine;

public class GiveHealthNode : ActionNode
{
    private int amount;

    public GiveHealthNode(BattleAction action, int amount)
        : base(action) 
    {
        this.amount = amount;
    }

    public override bool ApplyToCell(Cell cell)
    {
        Pawn target = cell.GetContent<Pawn>();
        if (target != null)
        {
            // Can only give as much health as actor has.
            int healthToGive = Mathf.Min(amount, action.Actor.Health);
            action.Actor.Health -= healthToGive;
            target.Health += healthToGive;
            
            return true;
        }

        return false;
    }
}
