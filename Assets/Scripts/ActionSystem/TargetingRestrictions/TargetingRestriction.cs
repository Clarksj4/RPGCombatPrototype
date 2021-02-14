using System;

[Serializable]
public abstract class TargetingRestriction
{
    public abstract bool IsTargetValid(Pawn actor, Cell cell); 
}
