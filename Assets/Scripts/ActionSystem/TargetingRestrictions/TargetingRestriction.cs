using System;

[Serializable]
public abstract class TargetingRestriction : IBattleActionElement
{
    public string name { get { return GetType().Name; } }
    public abstract bool IsTargetValid(Pawn actor, Cell cell); 
}
