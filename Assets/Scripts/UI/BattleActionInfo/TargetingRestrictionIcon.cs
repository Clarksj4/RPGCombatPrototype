using UnityEngine;
using UnityEngine.UI;

public class TargetingRestrictionIcon : MonoBehaviour
{
    public string DisplaysFor;
    protected Image image;

    public virtual void Setup(TargetingRestriction restriction)
    {
        image.sprite = SpriteManager.Instance.GetSpriteByName(restriction.GetType().Name);
    }
}
