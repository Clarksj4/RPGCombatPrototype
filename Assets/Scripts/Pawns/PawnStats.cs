using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Pawn Stats")]
public class PawnStats : ScriptableObject
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
    public List<ClassTable> Statuses = null;

    [Header("Actions")]
    public int MaxMana;
    public List<string> Actions;

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
        pawn.Actions = Actions;

        // Apply statuses
        if (Statuses != null)
        {
            foreach (ClassTable table in Statuses)
            {
                object status = table.CreateClass();
                pawn.AddStatus((PawnStatus)status);
            }
        }
    }
}
