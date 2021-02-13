using UnityEngine;
using System.Collections;
using TMPro;

public class HealthRestrictionDisplay : TargetingRestrictionDisplay
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI text;

    public override void Setup(TargetingRestriction restriction)
    {
        HealthRestriction manaRestriction = restriction as HealthRestriction;
        text.text = manaRestriction.Amount.ToString();
    }
}
