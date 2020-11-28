using System;

[Flags]
public enum ActionTag
{
    None = 0,
    Movement = 1,
    Damage = 2,
    Forced = 4,
    Heal = 8,
    AoE = 16
}
