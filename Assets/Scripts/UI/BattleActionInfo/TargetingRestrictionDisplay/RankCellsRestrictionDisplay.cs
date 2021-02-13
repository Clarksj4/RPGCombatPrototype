using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class RankCellsRestrictionDisplay : TargetingRestrictionDisplay
{
    [Header("Components")]
    [SerializeField] private string baseSpriteName;
    [SerializeField] private Image image;

    public override void Setup(TargetingRestriction restriction)
    {
        RankCellsRestriction rankRestriction = restriction as RankCellsRestriction;
        string spriteName = GetSpriteName(rankRestriction);
        image.sprite = SpriteManager.Instance.GetSpriteByName(spriteName);
    }

    private string GetSpriteName(RankCellsRestriction restriction)
    {
        string spriteName = baseSpriteName;
        foreach (int rank in restriction.Ranks.OrderBy(r => r))
            spriteName += rank.ToString();
        return spriteName;
    }
}
