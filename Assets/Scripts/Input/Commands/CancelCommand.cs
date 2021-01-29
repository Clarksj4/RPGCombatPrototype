public class CancelCommand : Command
{
    public override void Do()
    {
        if (ActionManager.Instance.HasTarget)
            ActionManager.Instance.ClearSelectedTarget();
        else if (ActionManager.Instance.HasAction)
            ActionManager.Instance.ClearSelectedAction();
    }
}