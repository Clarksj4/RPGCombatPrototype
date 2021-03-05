using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pawn", menuName = "Pawn Data")]
public class PawnData : SerializedScriptableObject
{
    [HideLabel, PreviewField(100)]
    public Sprite HeadSprite;

    [HideLabel, PreviewField(100)]
    public Sprite SplashSprite;

    [Tooltip("The stats associate with the attached pawn.")]
    public Stat[] Stats = new Stat[]
    {
        new Stat() { Name = "Health", Value = 100 },
        new Stat() { Name = "Mana", Value = 0 },
        new Stat() { Name = "Priority", Value = 50 },
        new Stat() { Name = "Movement", Value = 1 },
        new Stat() { Name = "ActionsPerTurn", Value = 1 }
    };

    [OdinSerialize]
    [Tooltip("Collection of statuses currently applied to this pawn.")]
    public List<PawnStatus> Statuses = new List<PawnStatus>(0);

    [Tooltip("The actions that this actor can do.")]
    public List<BattleAction> Actions = new List<BattleAction>(0);

    public virtual void SetData(Pawn pawn)
    {
        pawn.name = name;

        // Duplicate each stat for actor
        if (Stats != null)
        {
            foreach (Stat stat in Stats)
            {
                pawn.Stats.Add(new Stat() { 
                    Name = stat.Name,
                    Value = stat.Value,
                    Min = stat.Min,
                    Max = stat.Max
                });
            }
                
        }

        // Give actor a duplicate of each action.
        if (Actions != null)
        {
            foreach (BattleAction action in Actions)
            {
                BattleAction duplicate = Instantiate(action);
                pawn.Actions.Add(duplicate);
            }
        }

        // Apply each status to the pawn
        if (Statuses != null)
        {
            foreach (PawnStatus status in Statuses)
                pawn.Statuses.Add(status);
        }
    }
}
