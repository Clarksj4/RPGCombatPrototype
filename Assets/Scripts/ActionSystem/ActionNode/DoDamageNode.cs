using UnityEngine;

public class DoDamageNode : ActionNode
{
    public DoDamageNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Formation formation, Vector2Int position)
    {
        IDefender defender = formation.GetPawnAtCoordinate(position);

        if (defender != null)
        {
            // Damage can't be below 0
            int damage = (int)Mathf.Max(0, action.Actor.Attack - defender.Defense);
            Debug.Log($"Defender takes {damage} damage.");
            defender.Health -= damage;
            return true;
        }

        return false;
    }
}
