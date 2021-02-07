
public class FileCellsRestriction : TargetingRestriction
{
    public override bool IsTargetValid(Pawn actor, Cell cell)
    {
        return cell.Formation.GetFile(cell.Coordinate) == actor.File;
    }
}
