using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Pawn Stats")]
public class PawnStats : SerializedScriptableObject
{
    [Header("Initiative")]
    public float Priority = 0;

    [Header("Defense")]
    public int Defense;
    public int MaxHealth;

    [Header("Attack")]
    public float Power = 1.0f;

    [Header("Movement")]
    public int Movement;

    [Header("Statuses")]
    [OdinSerialize]
    public List<PawnStatus> Statuses = null;

    [Header("Actions")]
    public int MaxMana;
    public List<BattleAction> BattleActions;

    public virtual void SetStats(Pawn pawn)
    {
        pawn.name = name;
        pawn.Defense = Defense;
        pawn.MaxHealth = MaxHealth;
        pawn.Health = MaxHealth;
        pawn.Priority = Priority;
        pawn.Power = Power;
        pawn.Movement = Movement;
        pawn.MaxMana = MaxMana;
        pawn.BattleActions = BattleActions;

        // Duplicate each action and give it to the pawn.        
        if (BattleActions != null)
            foreach (BattleAction action in BattleActions)
                pawn.BattleActions.Add(Instantiate(action));

        // Apply each status to the pawn
        if (Statuses != null)
            foreach (PawnStatus status in Statuses)
                pawn.AddStatus(status);
    }
}
