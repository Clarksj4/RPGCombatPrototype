using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Color canAffordColour;
    public Color cantAffordColour;
    public Color angryCantAffordColour;
    public float angryDuration;

    public GameObject ManaCost;
    public TextMeshProUGUI ManaCostText;
    public GameObject Cooldown;
    public TextMeshProUGUI CooldownText;
    public Image AbilityImage;
    public Button Button;

    private Sequence angrySequence;
    private BattleAction action;

    private void Awake()
    {
        ActionManager.Instance.OnActionComplete += HandleOnActionComplete;
    }

    public void SetAction(BattleAction action)
    {
        this.action = action;
        Refresh();
    }

    public void Refresh()
    {
        if (action != null)
        {
            RefreshManaCost();
            RefreshUsability();


            if (action.Sprite != null)
                AbilityImage.sprite = action.Sprite;
        }
    }

    public void OnTap()
    {
        if (!action.CanDo())
        {
            if (!CanAfford())
                HighlightCost();
        }

        else
            ActionManager.Instance.SelectAction(action);
    }

    private bool CanAfford()
    {
        ManaRestriction restriction = GetManaRestriction();
        if (restriction != null)
            return action.Actor.Stats["Mana"].Value >= restriction.Amount;

        return true;
    }

    private ManaRestriction GetManaRestriction()
    {
        if (action != null && action.ActorRestrictions != null)
            return action.ActorRestrictions.FirstOfTypeOrDefault<TargetingRestriction, ManaRestriction>();

        return null;
    }

    private void RefreshUsability()
    {
        if (action.CanDo() && action.AnyTargetableCells)
        {
            Button.targetGraphic.color = Button.colors.normalColor;
            Button.transition = Selectable.Transition.ColorTint;
        }
            
        else
        {
            Button.targetGraphic.color = Button.colors.disabledColor;
            Button.transition = Selectable.Transition.None;
        }
            
    }

    private void RefreshManaCost()
    {
        // Show mana restriction if there is one
        ManaRestriction restriction = GetManaRestriction();
        bool hasManaRestriction = restriction != null;
        ManaCost.SetActive(hasManaRestriction);
        if (hasManaRestriction)
        {
            // Set text and colour
            ManaCostText.text = restriction.Amount.ToString();
            ManaCostText.color = CanAfford() ? canAffordColour : cantAffordColour;
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
            
        float startScale = ManaCostText.transform.localScale.x;
        angrySequence = DOTween.Sequence();
        angrySequence.Append(ManaCostText.DOColor(angryCantAffordColour, angryDuration * 0.5f));
        angrySequence.Join(ManaCostText.transform.DOScale(startScale * 1.25f, angryDuration * 0.5f).SetEase(Ease.OutQuad));
        angrySequence.Append(ManaCostText.DOColor(cantAffordColour, angryDuration * 0.5f));
        angrySequence.Join(ManaCostText.transform.DOScale(startScale, angryDuration * 0.5f).SetEase(Ease.InQuad));
    }

    private void HandleOnActionComplete(Pawn pawn, BattleAction action)
    {
        if (gameObject.activeSelf)
            Refresh();
    }
}
