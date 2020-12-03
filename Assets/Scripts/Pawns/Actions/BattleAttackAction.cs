using UnityEngine;



public abstract class BattleAttackAction : BattleAction
{
    /// <summary>
    /// The minimum chance for an attack to hit.
    /// </summary>
    private const float MINIMUM_HIT_CHANCE = 0.1f;

    protected virtual bool Attack(IDefender defender)
    {
        if (defender != null)
        {
            bool isHit = IsHit(defender);
            if (isHit)
                ApplyDamage(defender);
            return isHit;
        }

        return false;
    }

    // Does the attack hit?
    protected virtual bool IsHit(IDefender defender)
    {
        // Always have a minimum chance to hit
        float hitChance = 1 - Mathf.Max(MINIMUM_HIT_CHANCE, Actor.Accuracy - defender.Evasion);
        float roll = Random.Range(0f, 1f);
        bool hits = roll >= hitChance;
        Debug.Log($"Roll: {roll} vs {hitChance}. Attack hits: {hits}!");
        return hits;
    }

    // How much damage does the attack do?
    protected virtual void ApplyDamage(IDefender defender)
    {
        // Damage can't be below 0
        int damage = (int)Mathf.Max(0, Actor.Attack - defender.Defense);
        Debug.Log($"Defender takes {damage} damage.");
        defender.Health -= damage;
    }

}
