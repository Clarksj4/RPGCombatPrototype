using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RangeRestrictionDisplay : TargetingRestrictionDisplay
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI text;

    public override void Setup(TargetingRestriction restriction)
    {
        RangeRestriction rangeRestriction = restriction as RangeRestriction;
        text.text = rangeRestriction.Range.ToString();
    }
}
