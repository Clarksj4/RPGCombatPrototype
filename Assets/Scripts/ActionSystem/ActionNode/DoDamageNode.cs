using System.Linq;
using UnityEngine;

public class DoDamageNode : ActionNode
{
    public DoDamageNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell originCell, Cell targetCell)
    {
        Actor attacker = originCell.GetContent<Actor>();
        Pawn defender = targetCell.GetContent<Pawn>();

        if (attacker != null && defender != null)
        {
            // Damage can't be below 0
            int damage = (int)Mathf.Max(0, attacker.Attack - defender.Defense);
            Debug.Log($"{attacker.name} deals {defender.name} {damage} damage.");
            defender.Health -= damage;

            return true;
        }

        return false;
    }
}
