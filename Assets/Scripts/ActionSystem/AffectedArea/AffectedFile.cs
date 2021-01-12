using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AffectedFile : AffectedArea
{
    private Func<int> getFile;

    public AffectedFile(BattleAction action, Func<int> getFile)
        : base(action)
    {
        this.getFile = getFile;
    }

    public override IEnumerable<Cell> GetAffectedArea()
    {
        Formation formation = action.TargetCell.Formation;
        foreach (Cell cell in formation.GetFileCells(getFile()))
            yield return cell;
    }
}
