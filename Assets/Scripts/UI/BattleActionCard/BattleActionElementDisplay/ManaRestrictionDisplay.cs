using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class ManaRestrictionDisplay : BattleActionElementDisplay
{
    [BoxGroup("Components")]
    [SerializeField] private TextMeshProUGUI text;
    
    public override void Setup(IBattleActionElement element)
    {
        ManaRestriction manaRestriction = element as ManaRestriction;
        text.text = manaRestriction.Amount.ToString();
    }
}
