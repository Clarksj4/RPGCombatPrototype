using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionInfoMenu : Menu
{
    [SerializeField] private TextMeshProUGUI actionNameTMP = null;
    [SerializeField] private TextMeshProUGUI actionDescriptionTMP = null;
    [SerializeField] private Image actionImage = null;

    protected override void Awake()
    {
        base.Awake();

        ActionManager.Instance.OnActionSelected += HandleOnActionSelected;
        ActionManager.Instance.OnActionDeselected += HandleOnActionDeslected;
    }

    public void OnCancelTapped()
    {
        if (ActionManager.Instance.HasAction)
            ActionManager.Instance.ClearSelectedAction();
    }

    private void HandleOnActionSelected(BattleAction action)
    {
        RefreshContent(action);

        MenuStack.Instance.Show(this);
    }

    private void HandleOnActionDeslected(BattleAction action)
    {
        MenuStack.Instance.Hide();
    }

    private void RefreshContent(BattleAction action)
    {
        actionNameTMP.text = action.name;
        actionDescriptionTMP.text = action.Description;
        actionImage.sprite = action.Sprite;
    }
}
