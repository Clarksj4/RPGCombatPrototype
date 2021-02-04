using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RangeRestrictionIcon : TargetingRestrictionIcon
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI text;

    public override void Setup(TargetingRestriction restriction)
    {
        image.sprite = SpriteManager.Instance.GetSpriteByName(restriction.GetType().Name);
        RangeRestriction rangeRestriction = restriction as RangeRestriction;
        text.text = rangeRestriction.Range.ToString();
    }
}
