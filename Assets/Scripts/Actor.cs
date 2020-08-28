using System.Collections.Generic;

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
    /// <summary>
    /// Gets the amount of damage this actor can do with each
    /// attack.
    /// </summary>
    public float Attack { get; private set; } = 10f;
    /// <summary>
    /// Gets how accurate this actor with their actions.
    /// </summary>
    public float Accuracy { get; private set; } = 1f;
    /// <summary>
    /// Gets how far this actor can move in their turn.
    /// </summary>
    public int Movement { get; private set; } = 3;
    /// <summary>
    /// Gets the range on this actors attacks.
    /// </summary>
    public int AttackRange { get; private set; } = 1;
    /// <summary>
    /// Gets the actions available to this actor.
    /// </summary>
    public List<string> Actions { get; private set; } = new List<string>()
    {
        "Move", "Attack", "Push"
    };
    /// <summary>
    /// Ends this actors turn.
    /// </summary>
    public void EndTurn()
    {
        // Thing!
    }
}
