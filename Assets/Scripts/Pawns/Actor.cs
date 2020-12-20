using System;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Pawn, ITurnBased, IAttacker, ITeamBased
{
    [SerializeField] private float priority = 90f;
    [SerializeField] private float attack = 10f;
    [SerializeField] private float accuracy = 1f;
    [SerializeField] private int movement = 3;
    [SerializeField] private int reach = 1;
    [SerializeField] private List<string> actions = new List<string>() { "Move", "Attack", "Push" };
    /// <summary>
    /// Occurs when this actor's allegience changes.
    /// </summary>
    public event Action OnTeamChanged;

    /// <summary>
    /// Gets this actors priority in the turn order. Determines
    /// how frequently it gets to act.
    /// </summary>
    public float Priority { get { return priority; } }
    /// <summary>
    /// Gets whether this actor is currently able to take actions.
    /// </summary>
    public bool Incapacitated { get; set; }
    /// <summary>
    /// Gets the amount of damage this actor can do with each
    /// attack.
    /// </summary>
    public float Attack { get { return attack; } }
    /// <summary>
    /// Gets how accurate this actor with their actions.
    /// </summary>
    public float Accuracy { get { return accuracy; } }
    /// <summary>
    /// Gets how far this actor can move in their turn.
    /// </summary>
    public int Movement { get { return movement; } }
    /// <summary>
    /// Gets the range on this actors attacks.
    /// </summary>
    public int Reach { get { return reach; } }
    /// <summary>
    /// Gets the actions available to this actor.
    /// </summary>
    public List<string> Actions { get { return actions; } }
    /// <summary>
    /// Gets or sets the team this actor belongs to.
    /// </summary>
    public Team Team 
    {
        get { return team; }
        set
        {
            team = value;
            OnTeamChanged?.Invoke();
        }
    }
    private Team team;

    private void Awake()
    {
        TurnManager.Instance.Add(this);
    }

    /// <summary>
    /// Ends this actors turn.
    /// </summary>
    public void EndTurn()
    {
        // Thing!
    }
}
