using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAction : BattleAction
{
    /// <summary>
    /// The minimum chance for an attack to hit.
    /// </summary>
    private const float MINIMUM_HIT_CHANCE = 0.1f;
    /// <summary>
    /// The area from the targeted cell that will be affected.
    /// </summary>
    private const int AREA = 1;

    public override ActionTag Tags { get { return ActionTag.Damage; } }
    public override Target Target { get { return Target.Enemy | Target.Area; } }
    public override FormationTarget FormationTarget { get { return FormationTarget.Other; } }

    public override IEnumerable<(Formation, Vector2Int)> GetAffectedCoordinates()
    {
        foreach (Vector2Int coordinate in TargetFormation.GetCoordinatesInRange(TargetPosition, AREA))
            yield return (TargetFormation, coordinate);
    }

    public override IEnumerator Do()
    {
        foreach (Vector2Int coordinate in GetAffectedCoordinates())
        {
            Pawn defender = TargetFormation.GetPawnAtCoordinate(coordinate);
            if (defender != null && IsHit(defender))
                ApplyDamage(defender);
        }

        return null;
    }

    // Does the attack hit?
    private bool IsHit(Pawn defender)
    {
        // Always have a minimum chance to hit
        float hitChance = Mathf.Max(MINIMUM_HIT_CHANCE, Actor.Accuracy - defender.Evasion);
        bool hits = Random.Range(0f, 1f) <= hitChance;
        Debug.Log("Attack hits!");
        return hits;
    }

    // How much damage does the attack do?
    private void ApplyDamage(Pawn defender)
    {
        // Damage can't be below 0
        int damage = (int)Mathf.Max(0, Actor.Attack - defender.Defense);
        Debug.Log($"Defender takes {damage} damage.");
        defender.Health -= damage;
    }
}
