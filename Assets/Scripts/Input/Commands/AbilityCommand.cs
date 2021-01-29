public class AbilityCommand : Command
{
    public int AbilityIndex { get; set; }
    public string AbilityName { get; set; }

    public override void Do()
    {
        if (ActionManager.Instance.HasActor)
        {
            if (!string.IsNullOrEmpty(AbilityName))
                ActionManager.Instance.SelectAction(AbilityName);
            else
                ActionManager.Instance.SelectAction(AbilityIndex);

        }
    }
}