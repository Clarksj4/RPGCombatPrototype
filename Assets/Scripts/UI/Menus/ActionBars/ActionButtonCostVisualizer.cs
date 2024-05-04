using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonCostVisualizer : SerializedMonoBehaviour
{
    [OdinSerialize]
    private TargetingRestriction restriction = null;

    [SerializeField]
    private ActionButton actionButton = null;
    [Header("Components")]
    [SerializeField]
    private GameObject costObject = null;
    [SerializeField]
    private TextMeshProUGUI costText = null;
    [Header("Highlight")]
    [SerializeField]
    private Color canAffordColour = default;
    [SerializeField]
    private Color cantAffordColour = default;
    [SerializeField]
    private Color angryCantAffordColour = default;
    [SerializeField]
    private float angryDuration = 1f;
    [SerializeField]
    private float angrySize = 1.25f;

    private Sequence angrySequence;

    public void OnActionButtonTapped(bool activated)
    {
        if (!activated && !CanAfford())
            HighlightCost();
    }

    public void OnActionButtonRefresh()
    {
        RefreshManaCost();
    }

    private bool CanAfford()
    {
        TargetingRestriction restriction = GetRestriction();
        Pawn actor = actionButton.Action.Actor;
        Cell target = actionButton.Action.TargetCell;
        return restriction.IsTargetValid(actor, target);
    }

    private TargetingRestriction GetRestriction()
    {
        return actionButton.Action.ActorRestrictions.FirstOfTypeOrDefault(restriction.GetType());
    }

    private void RefreshManaCost()
    {
        // Show mana restriction if there is one
        TargetingRestriction restriction = GetRestriction();
        bool hasManaRestriction = restriction != null;
        costObject.SetActive(hasManaRestriction);
        if (hasManaRestriction)
        {
            // Set text and colour
            //costText.text = restriction.Amount.ToString();
            costText.color = CanAfford() ? canAffordColour : cantAffordColour;
        }
    }

    private void HighlightCost()
    {
        // Halt existing tween immediately (without finishing it)
        if (angrySequence != null && !angrySequence.IsComplete())
        {
            angrySequence.Complete();
            angrySequence = null;
        }

        float startScale = costText.transform.localScale.x;
        angrySequence = DOTween.Sequence();
        angrySequence.Append(costText.DOColor(angryCantAffordColour, angryDuration * 0.5f));
        angrySequence.Join(costText.transform.DOScale(startScale * angrySize, angryDuration * 0.5f).SetEase(Ease.OutQuad));
        angrySequence.Append(costText.DOColor(cantAffordColour, angryDuration * 0.5f));
        angrySequence.Join(costText.transform.DOScale(startScale, angryDuration * 0.5f).SetEase(Ease.InQuad));
    }
}