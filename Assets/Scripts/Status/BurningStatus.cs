using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class BurningStatus : PawnStatus
{
    /// <summary>
    /// Gets or sets the amount of damage that
    /// will be done to adjacent pawns.
    /// </summary>
    public int DamagePerTurn { get; set; }

    protected override void DoEffect()
    {
        // TODO: get all adjacent cells
        // TODO: do damage to them.
        IEnumerable<Cell> adjacentCells = Pawn.Grid.GetRange(Pawn.Coordinate, 1, 1);
        foreach (Cell cell in adjacentCells)
        {
            Pawn defender = cell.GetContent<Pawn>();
            if (defender != null)
                defender.TakeDamage(5, true);
        }
    }
}
