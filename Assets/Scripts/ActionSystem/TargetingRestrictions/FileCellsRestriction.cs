using UnityEngine;
using System.Collections.Generic;
using System;

public class FileCellsRestriction : TargetingRestriction
{
    private Func<int> getFile;

    public FileCellsRestriction(BattleAction action, Func<int> getFile)
        : base(action)
    {
        this.getFile = getFile;
    }

    public override bool IsTargetValid(Cell cell)
    {
        int file = getFile();
        return cell.Formation.GetFile(cell.Coordinate) == file;
    }
}
