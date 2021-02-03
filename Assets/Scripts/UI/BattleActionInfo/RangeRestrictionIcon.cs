using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RangeRestrictionIcon : TargetingRestrictionIcon
{
    public TextMeshProUGUI text;

    public override void Setup(TargetingRestriction restriction)
    {
        base.Setup(restriction);

        RangeRestriction rangeRestriction = restriction as RangeRestriction;
        text.text = rangeRestriction.Range.ToString();
    }
}
