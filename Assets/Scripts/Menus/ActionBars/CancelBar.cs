public class CancelBar : Menu
{
    protected override void Awake()
    {
        base.Awake();

        ActionManager.Instance.OnActionSelected += HandleOnActionSelected;
        ActionManager.Instance.OnActionDeselected += HandleOnActionDeslected;
    }

    public void OnCancelTapped()
    {
        ActionManager.Instance.ClearSelectedAction();
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
