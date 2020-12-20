using System.Linq;
using UnityEngine;

public class IsHitNode : ActionNode
{
    /// <summary>
    /// The minimum chance for an attack to hit.
    /// </summary>
    private const float MINIMUM_HIT_CHANCE = 0.1f;

    public IsHitNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell cell)
    {
        foreach (IGridBased target in cell.Contents)
        {
            if (target is IDefender)
            {
                IDefender defender = target as IDefender;

                // Always have a minimum chance to hit
                float hitChance = 1f - Mathf.Max(MINIMUM_HIT_CHANCE, action.Actor.Accuracy - defender.Evasion);
                float roll = Random.Range(0f, 1f);
                bool hits = roll >= hitChance;
                Debug.Log($"Roll: {roll} vs {hitChance}. Attack hits: {hits}!");
                
                return true;
            }
        }
        
        return false;
    }
}
