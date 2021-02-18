using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class RankCellsRestrictionDisplay : BattleActionElementDisplay
{
    [BoxGroup("Components")]
    [SerializeField] private string baseSpriteName;

    public override void Setup(IBattleActionElement element)
    {
        RankCellsRestriction rankRestriction = element as RankCellsRestriction;
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
