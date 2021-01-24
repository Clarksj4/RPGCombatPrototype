
public class FileCellsRestriction : TargetingRestriction
{
    /// <summary>
    /// Gets or sets the file that the targeted cells
    /// much be on to be valid.
    /// </summary>
    public int File { get; set; }

    public override bool IsTargetValid(Cell cell)
    {
        return cell.Formation.GetFile(cell.Coordinate) == File;
    }
}
