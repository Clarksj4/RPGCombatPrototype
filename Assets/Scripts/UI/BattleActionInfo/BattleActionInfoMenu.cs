using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class BattleActionInfoMenu : Menu
{
    [Header("Menu Components")]
    [Tooltip("Text for showing the name of the displayed action.")]
    [SerializeField] private TextMeshProUGUI actionTitle;
    [Tooltip("Image for showing the sprite of the displayed action.")]
    [SerializeField] private Image actionImage;
    [Tooltip("Layout for showing the targeting restrictions on the displayed action.")]
    [SerializeField] private TargetingRestrictionDisplayLayout restrictionsLayout;

    protected override void Awake()
    {
        base.Awake();
        
        ActionManager.Instance.OnActionSelected += HandleOnActionSelected;
        ActionManager.Instance.OnActionDeselected += HandleOnActionDeselected;

    }

    private void HandleOnActionSelected(BattleAction action)
    {
        SetAction(action);
        Show();
    }

    private void HandleOnActionDeselected(BattleAction action)
    {
        Hide();
    }

    public void SetAction(BattleAction action)
    {
        // TODO: mana is a special case
        actionTitle.text = action.name;

        actionImage.sprite = SpriteManager.Instance.GetSpriteByName(action.name);

        restrictionsLayout.Display(action.TargetingRestrictions);
    }
}
