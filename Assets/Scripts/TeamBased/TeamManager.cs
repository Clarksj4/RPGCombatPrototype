using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TeamManager : Singleton<TeamManager>
{
    public IList<Team> Teams { get; private set; } = new List<Team>();

    public void AddTeam(Team team)
    {
        if (Teams.Any(t => t.Name == team.Name))
            throw new ArgumentException($"A team with name: {team.Name} already exists.");

        Teams.Add(team);
    }

    public void AddTeamAndMembers(string teamName, Color teamColour, params ITeamBased[] members)
    {
        // Create new team
        Team team = new Team()
        {
            Name = teamName,
            Colour = teamColour
        };

        // Cache it
        Teams.Add(team);

        // Add members to team
        foreach (ITeamBased member in members)
            team.Add(member);
    }
}
