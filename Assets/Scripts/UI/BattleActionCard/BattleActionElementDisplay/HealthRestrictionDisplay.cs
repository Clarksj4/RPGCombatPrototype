using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class HealthRestrictionDisplay : BattleActionElementDisplay
{
    [BoxGroup("Components")]
    [SerializeField] private TextMeshProUGUI text = null;

    public override void Setup(IBattleActionElement element)
    {
        HealthRestriction manaRestriction = element as HealthRestriction;
        text.text = manaRestriction.Amount.ToString();
    }
}
