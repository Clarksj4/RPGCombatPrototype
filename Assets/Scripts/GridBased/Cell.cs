using UnityEngine;
using System.Collections;

public class Cell
{
    public GridRefactor Parent { get; private set; }
    public bool Passable { get; private set; }

    public Cell(GridRefactor parent, bool passable)
    {
        Parent = parent;
        Passable = passable;
    }
}
