using System.Collections.Generic;

public class AffectedRange : AffectedArea
{
    public int Min;
    public int Max;

    public override IEnumerable<Cell> GetAffectedArea(Cell targetedCell)
    {
        foreach (Cell cell in targetedCell.Grid.GetRange(targetedCell.Coordinate, Min, Max))
            yield return cell;
    }
}
