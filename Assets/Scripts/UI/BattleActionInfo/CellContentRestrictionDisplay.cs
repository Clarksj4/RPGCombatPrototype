using UnityEngine;

public class CellContentRestrictionDisplay : TargetingRestrictionDisplay
{
    [Header("Components")]
    [SerializeField] private GameObject all;
    [SerializeField] private GameObject ally;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject self;
    [SerializeField] private GameObject empty;

    public override void Setup(TargetingRestriction restriction)
    {
        // By default, hide all the bits
        ShowAll(false);

        CellContentRestriction cellRestriction = restriction as CellContentRestriction;
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
