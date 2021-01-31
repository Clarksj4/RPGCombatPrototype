
public class MoveNode : ActionNode
{
    public override bool Do(Pawn actor, Cell target)
    {
        actor.Move(target);
        return true;
    }
}
