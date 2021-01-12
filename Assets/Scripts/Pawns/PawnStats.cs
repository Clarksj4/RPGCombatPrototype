using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Pawn Stats")]
public class PawnStats : ScriptableObject
{
    [Header("Defense")]
    public float Defense;
    public float Evasion;
    public int MaxHealth;

    public virtual void SetStats(Pawn pawn)
    {
        pawn.name = name;
        pawn.Defense = Defense;
        pawn.Evasion = Evasion;
        pawn.MaxHealth = MaxHealth;
        pawn.Health = MaxHealth;
    }
}
