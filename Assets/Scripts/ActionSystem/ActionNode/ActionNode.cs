using System;

[Serializable]
public abstract class ActionNode : IBattleActionElement
{
    public string name { get { return GetType().Name; } }

    public abstract bool Do(Pawn actor, Cell target);
}
