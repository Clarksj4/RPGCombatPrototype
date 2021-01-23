
public class MoveNode : ActionNode
{
    public override bool Do()
    {
        Actor.Move(Target);
        return true;
    }
}
