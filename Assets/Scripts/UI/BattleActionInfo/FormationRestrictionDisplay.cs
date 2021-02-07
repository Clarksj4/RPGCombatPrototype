using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FormationRestrictionDisplay : TargetingRestrictionDisplay
{
    [Header("Components")]
    [SerializeField] private Image image;

    [Header("Colours")]
    [SerializeField] private Color selfColour;
    [SerializeField] private Color otherColour;

    public override void Setup(TargetingRestriction restriction)
    {
        FormationRestriction formationRestriction = restriction as FormationRestriction;
        TargetableFormation formations = formationRestriction.Formations;

        if (formations.HasFlag(TargetableFormation.Other) &&
           !formations.HasFlag(TargetableFormation.Self))
            image.color = otherColour;

        else if (formations.HasFlag(TargetableFormation.Self) &&
                !formations.HasFlag(TargetableFormation.Other))
            image.color = selfColour;
    }
}