using System;

[Flags]
public enum Target
{
    None = 0,
    Self = 1,
    Ally = 2,
    Enemy = 4,
    Area = 8,
    All = 16
}
