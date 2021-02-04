using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CellContentRestrictionIcon : TargetingRestrictionIcon
{
    [SerializeField]
    private Image[] images;

    public override void Setup(TargetingRestriction restriction)
    {
        int i = 0;
        CellContentRestriction cellRestriction = restriction as CellContentRestriction;
        TargetableCellContent content = cellRestriction.Content;

        if (content.HasFlag(TargetableCellContent.All))
        {
            images[i].sprite = SpriteManager.Instance.GetSpriteByName("HumanIcon");
            images[i].color = Color.grey;
            i++;
        }

        else
        {
            if (content.HasFlag(TargetableCellContent.Ally))
            {
                images[i].sprite = SpriteManager.Instance.GetSpriteByName("HumanIcon");
                images[i].color = Color.green;
                i++;
            }

            if (content.HasFlag(TargetableCellContent.Enemy))
            {
                images[i].sprite = SpriteManager.Instance.GetSpriteByName("HumanIcon");
                images[i].color = Color.green;
                i++;
            }

            if (content.HasFlag(TargetableCellContent.Self))
            {
                images[i].sprite = SpriteManager.Instance.GetSpriteByName("HumanIcon");
                images[i].color = Color.blue;
                i++;
            }
        }

        if (content.HasFlag(TargetableCellContent.All) ||
            content.HasFlag(TargetableCellContent.Empty))
        {
            images[i].sprite = SpriteManager.Instance.GetSpriteByName("PointIcon");
            i++;
        }

        // Turn on or off images depending upon if they have an image.
        for (int j = 0; j < images.Length; j++)
            images[j].gameObject.SetActive(j < i);
    }
}
