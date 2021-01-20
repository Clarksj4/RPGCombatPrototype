using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : Pawn, ITurnBased, ITeamBased
{
    /// <summary>
    /// Occurs when this actor's allegience changes.
    /// </summary>
    public event Action OnTeamChanged;

    /// <summary>
    /// Gets whether this actor is currently able to take actions.
    /// </summary>
    public bool Incapacitated { get; set; }
    /// <summary>
    /// Gets how far this actor can move in their turn.
    /// </summary>
    public int Movement { get; set; }
    /// <summary>
    /// Gets the rank this actor is in.
    /// </summary>
    public int Rank { get { return Formation.GetRank(Coordinate); } }
    /// <summary>
    /// Gets the file this actor is in.
    /// </summary>
    public int File { get { return Formation.GetFile(Coordinate); } }
    /// <summary>
    /// Gets the actions available to this actor.
    /// </summary>
    public List<string> Actions { get; set; }
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

    protected override void Awake()
    {
        base.Awake();

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
