using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AffectedFile : AffectedArea
{
    private int file;

    public AffectedFile(BattleAction action, int file)
        : base(action)
    {
        this.file = file;
    }

    public override IEnumerable<Cell> GetAffectedArea()
    {
        Formation formation = action.TargetCell.Formation;
        foreach (Cell cell in formation.GetFileCells(file))
            yield return cell;
    }
}
