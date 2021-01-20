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

    public virtual void SetStats(Pawn pawn)
    {
        pawn.name = name;
        pawn.Defense = Defense;
        pawn.MaxHealth = MaxHealth;
        pawn.Health = MaxHealth;
        pawn.Priority = Priority;
        pawn.Power = Power;
    }
}
