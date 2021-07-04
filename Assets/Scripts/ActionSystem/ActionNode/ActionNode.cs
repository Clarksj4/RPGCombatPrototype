using System;
using SimpleBehaviourTree;

[Serializable]
public abstract class ActionNode : IBattleActionElement, IBehaviourTreeNode
{
    public string name { get { return GetType().Name; } }

    public abstract bool Do(Blackboard state);
}
