using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefaultTargetingRestrictionIcon : TargetingRestrictionIcon
{
    [SerializeField]
    private Image image;

    public override void Setup(TargetingRestriction restriction)
    {
        image.sprite = SpriteManager.Instance.GetSpriteByName(restriction.GetType().Name);
    }
}
