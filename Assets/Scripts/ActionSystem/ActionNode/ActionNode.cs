using System;

[Serializable]
public abstract class ActionNode
{
    public abstract bool Do(Pawn actor, Cell target);
}
