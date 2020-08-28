using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttacksBar : Menu
{
    private Button[] attackButtons;

    protected override void Awake()
    {
        base.Awake();

        attackButtons = GetComponentsInChildren<Button>();
    }

    protected override void PreShow()
    {
        base.PreShow();

        // TODO: populate bar actions with actor's attacks
    }

    public void OnAttackTapped(int index)
    {
        ActionManager.Instance.SelectAction<AttackAction>();
        MenuStack.Instance.Show<CancelBar>();
    }

    public void OnCancelTapped()
    {
        ActionManager.Instance.ClearSelectedAction();
        MenuStack.Instance.Hide();
    }
}
