using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Pawn, ITurnBased, IAttacker
{
    /// <summary>
    /// Gets this actor's name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets this actors priority in the turn order. Determines
    /// how frequently it gets to act.
    /// </summary>
    public float Priority { get; set; }
    /// <summary>
    /// Gets whether this actor is currently able to take actions.
    /// </summary>
    public bool Incapacitated { get; private set; }

    public float Attack { get; private set; } = 10f;

    public float Accuracy { get; private set; } = 1f;
    public int Movement { get; private set; } = 3;
    public int AttackRange { get; private set; } = 1;
    public List<string> Actions { get; private set; } = new List<string>()
    {
        "Move", "Attack", "Push"
    };

    public void EndTurn()
    {
        // Thing!
    }
}
