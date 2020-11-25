
using System;

public interface ITeamBased
{
    event Action OnTeamChanged;
    Team Team { get; set; }
}
