using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAction : BattleAttackAction
{
    /// <summary>
    /// The area from the targeted cell that will be affected.
    /// </summary>
    private const int AREA = 1;

    public override ActionTag Tags { get { return ActionTag.Damage | ActionTag.AoE; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Enemy | TargetableCellContent.Empty; } }
    public override TargetableFormation TargetableFormation { get { return TargetableFormation.Other; } }

    public FireballAction()
        : base()
    {
        targetableStrategy = new LinearExposedCells(this);
        targetedStrategy = new TargetedArea(this, 1);
    }

    public override IEnumerator Do()
    {
        foreach ((Formation formation, Vector2Int coordinate) in GetAffectedCoordinates())
        {
            Pawn defender = formation.GetPawnAtCoordinate(coordinate);
            Attack(defender);
        }

        return null;
    }
}
