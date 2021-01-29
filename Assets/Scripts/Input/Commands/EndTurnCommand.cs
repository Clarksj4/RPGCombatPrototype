public class EndTurnCommand : Command
{
    public override void Do()
    {
        if (!ActionManager.Instance.HasAction)
            ActionManager.Instance.EndSelectedActorTurn();
    }
}