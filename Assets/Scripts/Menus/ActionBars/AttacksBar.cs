using UnityEngine;
using System.Collections;

public class AttacksBar : Menu
{
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
        MenuStack.Instance.CloseCurrent();
    }
}
