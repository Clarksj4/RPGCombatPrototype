using UnityEngine;
using System.Collections;
using TMPro;

public class ManaRestrictionDisplay : TargetingRestrictionDisplay
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI text;
    
    public override void Setup(TargetingRestriction restriction)
    {
        ManaRestriction manaRestriction = restriction as ManaRestriction;
        text.text = manaRestriction.Amount.ToString();
    }
}
