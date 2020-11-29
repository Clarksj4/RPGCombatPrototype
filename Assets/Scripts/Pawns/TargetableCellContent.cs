using System;

[Flags]
public enum TargetableCellContent
{
    None = 0,
    Self = 1,
    Ally = 2,
    Enemy = 4,
    Empty = 8,
    All = 16
}
