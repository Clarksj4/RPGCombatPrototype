using Sirenix.OdinInspector;
using UnityEngine;

public class CellContentRestrictionDisplay : BattleActionElementDisplay
{
    [BoxGroup("Components")]
    [SerializeField] private GameObject all = null;
    [BoxGroup("Components")]
    [SerializeField] private GameObject ally = null;
    [BoxGroup("Components")]
    [SerializeField] private GameObject enemy = null;
    [BoxGroup("Components")]
    [SerializeField] private GameObject self = null;
    [BoxGroup("Components")]
    [SerializeField] private GameObject empty = null;

    public override void Setup(IBattleActionElement element)
    {
        // By default, hide all the bits
        ShowAll(false);

        CellContentRestriction cellRestriction = element as CellContentRestriction;
        TargetableCellContent content = cellRestriction.Content;

        // Everything!
        if (content.HasFlag(TargetableCellContent.All))
            all.SetActive(true);

        else
        {
            // Allies
            if (content.HasFlag(TargetableCellContent.Ally))
                ally.SetActive(true);

            // Enemies
            if (content.HasFlag(TargetableCellContent.Enemy))
                enemy.SetActive(true);

            // Self
            if (content.HasFlag(TargetableCellContent.Self))
                self.SetActive(true);
        }

        // Empty cells?
        if (content.HasFlag(TargetableCellContent.All) ||
            content.HasFlag(TargetableCellContent.Empty))
            empty.SetActive(true);
    }

    private void ShowAll(bool visible)
    {
        all.SetActive(visible);
        ally.SetActive(visible);
        enemy.SetActive(visible);
        self.SetActive(visible);
        empty.SetActive(visible);
    }


}
