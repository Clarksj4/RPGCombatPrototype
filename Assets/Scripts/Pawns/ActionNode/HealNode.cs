using UnityEngine;

public class HealNode : ActionNode
{
    public HealNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Formation formation, Vector2Int position)
    {
        Pawn pawn = formation.GetPawnAtCoordinate(position);
        if (pawn != null)
        {
            pawn.Health += 10;
            return true;
        }
            
        return false;
    }
}
