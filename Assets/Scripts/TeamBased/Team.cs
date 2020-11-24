using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team : IEnumerable<ITurnBased>
{
    public string Name { get; set; }
    public Color Colour { get; set; }

    private List<ITeamBased> members = new List<ITeamBased>();

    public void Add(ITeamBased member)
    {
        members.Add(member);
        member.Team = this;
    }

    public bool Remove(ITeamBased member)
    {
        return members.Remove(member);
    }

    public IEnumerator<ITurnBased> GetEnumerator()
    {
        return (IEnumerator<ITurnBased>)members;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return members.GetEnumerator();
    }
}
