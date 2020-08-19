using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    float Health { get; }
    float Defense { get; }

    void TakeDamage();
}
