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
    [SerializeField] private ActionButton actionButton = null;
    [SerializeField] private string resource = null;

    [Header("Components")]
    [SerializeField] private GameObject costObject = null;
    [SerializeField] private TextMeshProUGUI costAmountTMP = null;
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

    private BattleAction Action => actionButton.Action;
    private Pawn Actor => Action?.Actor;
    private int Cost => Action.GetCost(resource);
    private int CurrentAmount => Actor.Stats[resource].Value;

    private Sequence angrySequence;

    public void OnActionButtonTapped(bool activated)
    {
        if (!activated && !CanAfford())
            HighlightCost();
    }

    public void OnActionButtonRefresh()
    {
        RefreshCost();
    }

    private bool HasCost()
    {
        return Cost > 0;
    }

    private bool CanAfford()
    {
        return CurrentAmount >= Cost;
    }

    private void RefreshCost()
    {
        bool hasCost = HasCost();
        costObject.SetActive(hasCost);
        if (hasCost)
        {
            costAmountTMP.text = Cost.ToString();
            costAmountTMP.color = CanAfford() ? canAffordColour : cantAffordColour;
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

        float startScale = costAmountTMP.transform.localScale.x;
        angrySequence = DOTween.Sequence();
        angrySequence.Append(costAmountTMP.DOColor(angryCantAffordColour, angryDuration * 0.5f));
        angrySequence.Join(costAmountTMP.transform.DOScale(startScale * angrySize, angryDuration * 0.5f).SetEase(Ease.OutQuad));
        angrySequence.Append(costAmountTMP.DOColor(cantAffordColour, angryDuration * 0.5f));
        angrySequence.Join(costAmountTMP.transform.DOScale(startScale, angryDuration * 0.5f).SetEase(Ease.InQuad));
    }
}