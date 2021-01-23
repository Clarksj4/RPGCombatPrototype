using System;
using System.Collections.Generic;

public class Actor : Pawn, ITeamBased
{
    /// <summary>
    /// Occurs when this actor's allegience changes.
    /// </summary>
    public event Action OnTeamChanged;
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
}
