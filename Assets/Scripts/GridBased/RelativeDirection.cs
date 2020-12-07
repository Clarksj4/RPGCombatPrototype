using System;

[Flags]
public enum RelativeDirection
{
    None = 0,
    Towards = 1,
    Away = 2,
    Left = 4,
    Right = 8,
    All = 16
}
