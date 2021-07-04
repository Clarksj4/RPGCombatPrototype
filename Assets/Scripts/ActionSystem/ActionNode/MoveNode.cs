using SimpleBehaviourTree;

public class MoveNode : ActionNode
{
    public override bool Do(Blackboard state)
    {
        Pawn actor = state.Get<Pawn>("Actor");
        Cell target = state.Get<Cell>("Cell");
        Pawn occupant = target.GetContent<Pawn>();

        // Only move if the cell is empty
        bool canMove = occupant == null;
        if (canMove)
            actor.Move(target);

        return canMove;
    }
}
