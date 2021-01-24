using System.Linq;

public class RankCellsRestriction : TargetingRestriction
{
    /// <summary>
    /// Gets or sets the ranks that are valid targets.
    /// </summary>
    public int[] Ranks { get; set; }

    public override bool IsTargetValid(Cell cell)
    {
        return Ranks.Contains(cell.Formation.GetRank(cell.Coordinate));
    }
}
