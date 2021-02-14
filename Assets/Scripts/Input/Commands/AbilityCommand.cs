public class AbilityCommand : Command
{
    public int AbilityIndex { get; set; }
    public string AbilityName { get; set; }

    public override void Do()
    {
        if (ActionManager.Instance.HasActor)
        {
            if (!string.IsNullOrEmpty(AbilityName))
                ActionManager.Instance.SelectActionByName(AbilityName);
            else
                ActionManager.Instance.SelectActionByIndex(AbilityIndex);

        }
    }
}