using SimpleBehaviourTree;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BurningStatus : PawnStatus
{
    [Tooltip("The amount of damage that will be done to adjacent cells each turn.")]
    public int DamagePerTurn;

    public override PawnStatus Duplicate()
    {
        BurningStatus duplicate = base.Duplicate() as BurningStatus;
        duplicate.DamagePerTurn = DamagePerTurn;
        return duplicate;
    }

    protected override void DoEffect()
    {
        DoDamageNode damage = new DoDamageNode() { BaseDamage = DamagePerTurn };

        IEnumerable<Cell> adjacentCells = Pawn.Grid.GetRange(Pawn.Coordinate, 1, 1);
        foreach (Cell cell in adjacentCells)
        {
            Blackboard state = new Blackboard()
            {
                { "Actor", Pawn },
                { "Cell", cell }
            };

            damage.Do(state);
        }
    }
}
