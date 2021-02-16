using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Pawn Stats")]
public class PawnStats : SerializedScriptableObject
{
    [TitleGroup("Pawn")]
    [HorizontalGroup("Pawn/Split", LabelWidth = 100)]
    [VerticalGroup("Pawn/Split/Left")]
    [Tooltip("How much damage this pawn can take before perishing.")]
    public int MaxHealth = 100;

    [VerticalGroup("Pawn/Split/Left")]
    [Tooltip("How much mana this actor can store at once.")]
    public int MaxMana;

    [VerticalGroup("Pawn/Split/Left")]
    [Tooltip("Each attack has its damage reduced by this amount.")]
    public int Defense = 0;

    [VerticalGroup("Pawn/Split/Left")]
    [Tooltip("This pawn's priority in the turn order. A higher value acts sooner that a lower value.")]
    [Range(0f, 100f)]
    public float Priority = 0;

    [VerticalGroup("Pawn/Split/Left")]
    [Tooltip("Damage multiplier.")]
    public float Power = 1.0f;

    [VerticalGroup("Pawn/Split/Left")]
    [Tooltip("The number of cells this pawn can move per move action.")]
    public int Movement = 1;

    [VerticalGroup("Pawn/Split/Left")]
    [Tooltip("The maximum number of actions this pawn can do per turn.")]
    public int ActionsPerTurn = 1;

    [HorizontalGroup("Pawn/Split/Right", 100)]
    [VerticalGroup("Pawn/Split/Right/Inner")]
    [HideLabel, PreviewField(100)]
    public Sprite HeadSprite;

    [VerticalGroup("Pawn/Split/Right/Inner")]
    [HideLabel, PreviewField(100)]
    public Sprite SplashSprite;

    [OdinSerialize]
    [Tooltip("Collection of statuses currently applied to this pawn.")]
    public List<PawnStatus> Statuses = new List<PawnStatus>(0);

    [Tooltip("The actions that this actor can do.")]
    public List<BattleAction> BattleActions = new List<BattleAction>(0);


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
