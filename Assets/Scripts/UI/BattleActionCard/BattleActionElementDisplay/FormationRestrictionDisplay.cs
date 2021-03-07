using UnityEngine;
using Sirenix.OdinInspector;

public class FormationRestrictionDisplay : BattleActionElementDisplay
{
    [BoxGroup("Colours")]
    [SerializeField] private Color selfColour = Color.white;
    [SerializeField] private Color otherColour = Color.white;

    public override void Setup(IBattleActionElement element)
    {
        FormationRestriction formationRestriction = element as FormationRestriction;
        TargetableFormation formations = formationRestriction.Formations;

        if (formations.HasFlag(TargetableFormation.Other) &&
           !formations.HasFlag(TargetableFormation.Self))
            image.color = otherColour;

        else if (formations.HasFlag(TargetableFormation.Self) &&
                !formations.HasFlag(TargetableFormation.Other))
            image.color = selfColour;
    }
}