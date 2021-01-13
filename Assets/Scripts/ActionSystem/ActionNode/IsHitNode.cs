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

    public override bool ApplyToCell(Cell originCell, Cell targetCell)
    {
        Actor attacker = originCell.GetContent<Actor>();
        Pawn defender = targetCell.GetContent<Pawn>();

        if (attacker != null && defender != null)
        {
            // Always have a minimum chance to hit
            float hitChance = 1f - Mathf.Max(MINIMUM_HIT_CHANCE, attacker.Accuracy - defender.Evasion);
            float roll = Random.Range(0f, 1f);
            bool hits = roll >= hitChance;
            string hitInfo = hits ? "hits" : "misses";

            Debug.Log($"{attacker.name} {hitInfo} {defender.name}: Roll: {roll} vs {hitChance}.");

            return hits;
        }

        Debug.Log("Cell empty - counts as a hit.");
        // Counts as a hit.
        return true;
    }
}
