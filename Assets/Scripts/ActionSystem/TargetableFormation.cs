using System;

[Flags]
public enum TargetableFormation
{
    None = 0,
    Self = 1,
    Other = 2,
    All = 4
}
