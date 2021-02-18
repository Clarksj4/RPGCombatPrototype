using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System.Linq;

public class BattleActionCard : MonoBehaviour
{
    [SerializeField]
    private BattleAction Test;

    [BoxGroup("Components")]
    [Tooltip("Text for showing the name of the displayed action.")]
    [SerializeField] private TextMeshProUGUI actionTitle;
    [BoxGroup("Components")]
    [Tooltip("Image for showing the sprite of the displayed action.")]
    [SerializeField] private Image actionImage;
    [BoxGroup("Layouts")]
    [Tooltip("Layout for displaying the cost of using this action.")]
    [SerializeField] private SlotFiller actionCostLayout;
    [BoxGroup("Layouts")]
    [Tooltip("Layout for displaying the positional requirements of using this action.")]
    [SerializeField] private SlotFiller positionRestrictionLayout;
    [BoxGroup("Layouts")]
    [Tooltip("Layout for displaying the restrictions of valid targets.")]
    [SerializeField] private SlotFiller targetRestictionLayout;
    [BoxGroup("Layouts")]
    [Tooltip("Layout for displaying the effect on targeted cells.")]
    [SerializeField] private Transform actionLayout;

    private void OnValidate()
    {
        if (Application.isPlaying && Test != null)
            SetAction(Test);
    }

    public void SetAction(BattleAction action)
    {
        SetActionName(action);
        SetActionImage(action);
        DisplayActionCost(action);
        DisplayActionPositionRestrictions(action);
        DisplayActionTargetRestrictions(action);
        DisplayActionNodes(action);
    }

    private void SetActionName(BattleAction action)
    {
        actionTitle.text = action.name;
    }

    private void SetActionImage(BattleAction action)
    {
        actionImage.sprite = action.Sprite;
    }

    private void DisplayActionCost(BattleAction action)
    {
        var costs = action.ActorRestrictions.Where(r => r is ManaRestriction || 
                                                        r is HealthRestriction);
        foreach (IBattleActionElement element in costs)
        {
            BattleActionElementDisplay display = BattleActionElementDisplayManager.Instance.GetDisplay(element);
            actionCostLayout.Add(display.transform);
        }
    }

    private void DisplayActionPositionRestrictions(BattleAction action)
    {
        var nonCosts = action.ActorRestrictions.Where(r => !(r is ManaRestriction) && 
                                                           !(r is HealthRestriction));
        foreach (IBattleActionElement element in nonCosts)
        {
            BattleActionElementDisplay display = BattleActionElementDisplayManager.Instance.GetDisplay(element);
            positionRestrictionLayout.Add(display.transform);
        }
    }

    private void DisplayActionTargetRestrictions(BattleAction action)
    {
        foreach (IBattleActionElement element in action.TargetingRestrictions)
        {
            BattleActionElementDisplay display = BattleActionElementDisplayManager.Instance.GetDisplay(element);
            targetRestictionLayout.Add(display.transform);
        }
    }

    private void DisplayActionNodes(BattleAction action)
    {
        foreach (IBattleActionElement element in action.TargetedActions)
        {
            BattleActionElementDisplay display = BattleActionElementDisplayManager.Instance.GetDisplay(element);
            display.transform.SetParent(actionLayout);
        }
    }
}
