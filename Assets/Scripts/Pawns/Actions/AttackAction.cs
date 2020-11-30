using System.Collections;
using UnityEngine;

public class AttackAction : BattleAction
{
    /// <summary>
    /// The minimum chance for an attack to hit.
    /// </summary>
    private const float MINIMUM_HIT_CHANCE = 0.1f;

    public override int Range { get { return Actor.Reach; } }
    public override ActionTag Tags { get { return ActionTag.Damage; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Enemy; } }
    protected override TargetableCells TargetableCells { get { return targetableCells; } }
    private TargetableCells targetableCells;

    public AttackAction() 
        : base()
    {
        targetableCells = new AnyCells(this);
    }

    public override IEnumerator Do()
    {
        Pawn defender = TargetFormation.GetPawnAtCoordinate(TargetPosition);
        if (IsHit(defender))
            ApplyDamage(defender);

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
