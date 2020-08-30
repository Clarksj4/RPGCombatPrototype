using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BattleAction
{
    /// <summary>
    /// The minimum chance for an attack to hit.
    /// </summary>
    private const float MINIMUM_HIT_CHANCE = 0.1f;

    public override int Range { get { return Actor.AttackRange; } }

    public override ActionTag[] Tags { get { return tags; } }
    private ActionTag[] tags = new ActionTag[] { ActionTag.Damage };

    public override bool IsActorAble(Actor actor)
    {
        return !actor.Incapacitated;
    }
 
    public override bool IsTargetValid(BattleMap map, Vector2Int position)
    {
        Pawn target = map.GetPawnAtCoordinate(position);
        bool inRange = map.IsInRange(OriginPosition, position, Range);
        return target != null &&
                target != Actor &&
                inRange;
    }

    public override bool Do()
    {
        bool canDo = IsActorAble(Actor) && IsTargetValid(TargetMap, TargetPosition);

        if (canDo)
        {
            // Get defender, check if hit, and apply damage
            Pawn defender = TargetMap.GetPawnAtCoordinate(TargetPosition);
            if (IsHit(defender))
                ApplyDamage(defender);
        }

        else
            Debug.Log("Can't perform attack action.");

        return canDo;
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

    public override IEnumerable<Vector2Int> GetArea()
    {
        yield return TargetPosition;
    }
}
