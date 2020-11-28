using System;

public class CancelBar : Menu
{
    protected override void Awake()
    {
        base.Awake();

        ActionManager.Instance.OnActionSelected += HandleOnActionSelected;
        ActionManager.Instance.OnActionDeselected += HandleOnActionDeslected;
        ActionManager.Instance.OnTargetSelected += HandleOnTargetSelected;
        ActionManager.Instance.OnTargetDeselected += HandleOnTargetDeslected;
    }

    private void HandleOnTargetDeslected(BattleAction obj)
    {
        //MenuStack.Instance.Hide();
    }

    private void HandleOnTargetSelected(BattleAction obj)
    {
        if (MenuStack.Instance.Current != this)
            MenuStack.Instance.Show(this);
    }

    public void OnCancelTapped()
    {
        if (ActionManager.Instance.HasAction)
            ActionManager.Instance.ClearSelectedAction();
        else if (ActionManager.Instance.HasTarget)
            ActionManager.Instance.DeselectTarget();
    }

    private void HandleOnActionSelected(BattleAction obj)
    {
        MenuStack.Instance.Show(this);
    }

    private void HandleOnActionDeslected(BattleAction obj)
    {
        MenuStack.Instance.Hide();
    }
}
