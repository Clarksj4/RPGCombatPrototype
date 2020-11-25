using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Formation : Grid
{
    public IEnumerable<Pawn> Pawns { get { return pawns; } }

    private Pawn[] pawns;

    private void Awake()
    {
        pawns = GetComponentsInChildren<Pawn>();
    }

    /// <summary>
    /// Gets the first pawn at the given coordinate.
    /// </summary>
    public Pawn GetPawnAtCoordinate(Vector2Int coordinate)
    {
        return pawns.FirstOrDefault(a => a.GridPosition == coordinate);
    }
}
