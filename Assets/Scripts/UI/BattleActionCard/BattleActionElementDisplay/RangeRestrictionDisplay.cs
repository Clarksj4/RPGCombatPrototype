using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class RangeRestrictionDisplay : BattleActionElementDisplay
{
    [BoxGroup("Components")]
    [SerializeField] private TextMeshProUGUI text;

    public override void Setup(IBattleActionElement element)
    {
        RangeRestriction rangeRestriction = element as RangeRestriction;
        text.text = rangeRestriction.Range.ToString();
    }
}
