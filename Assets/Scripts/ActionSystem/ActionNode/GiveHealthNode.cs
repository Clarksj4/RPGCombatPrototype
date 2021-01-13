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

    public override bool ApplyToCell(Cell originCell, Cell targetCell)
    {
        Pawn giver = originCell.GetContent<Pawn>();
        Pawn receiver = targetCell.GetContent<Pawn>();

        if (giver != null && receiver != null)
        {
            // Can only give as much health as actor has.
            int healthToGive = Mathf.Min(amount, giver.Health);
            giver.Health -= healthToGive;
            receiver.Health += healthToGive;
            
            return true;
        }

        return false;
    }
}
