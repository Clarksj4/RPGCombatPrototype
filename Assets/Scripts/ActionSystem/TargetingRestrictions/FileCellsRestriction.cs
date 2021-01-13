
public class FileCellsRestriction : TargetingRestriction
{
    private int file;

    public FileCellsRestriction(BattleAction action, int file)
        : base(action)
    {
        this.file = file;
    }

    public override bool IsTargetValid(Cell cell)
    {
        return cell.Formation.GetFile(cell.Coordinate) == file;
    }
}
