using System;
using System.Collections.Generic;

public class Actor : Pawn, ITurnBased, ITeamBased
{
    /// <summary>
    /// Occurs when this actor's allegience changes.
    /// </summary>
    public event Action OnTeamChanged;
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

    protected override void TurnStart()
    {
        // If actor is incapacitated, it doesn't get a turn.
        // Hard luck, bud.
        if (Incapacitated)
            TurnManager.Instance.Next();
    }
}
