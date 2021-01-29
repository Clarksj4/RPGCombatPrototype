public class ConfirmCommand : Command
{
    public override void Do()
    {
        if (ActionManager.Instance.HasTarget)
            ActionManager.Instance.DoAction();
    }
}