using UnityEngine;

public class IsHitNode : ActionNode
{
    /// <summary>
    /// The minimum chance for an attack to hit.
    /// </summary>
    private const float MINIMUM_HIT_CHANCE = 0.1f;

    public IsHitNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Formation formation, Vector2Int position)
    {
        IDefender defender = formation.GetPawnAtCoordinate(position);

        if (defender != null)
        {
            // Always have a minimum chance to hit
            float hitChance = 1f - Mathf.Max(MINIMUM_HIT_CHANCE, action.Actor.Accuracy - defender.Evasion);
            float roll = Random.Range(0f, 1f);
            bool hits = roll >= hitChance;
            Debug.Log($"Roll: {roll} vs {hitChance}. Attack hits: {hits}!");
            return hits;
        }

        return false;
    }
}
