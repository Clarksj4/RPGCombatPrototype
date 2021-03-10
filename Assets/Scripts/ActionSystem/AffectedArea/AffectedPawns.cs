using System.Collections.Generic;

public class AffectedPawns : AffectedArea
{
    public override IEnumerable<Cell> GetAffectedArea(Cell targetedCell)
    {
        foreach (Formation formation in FormationManager.Instance.Formations)
            foreach (Pawn pawn in formation.Pawns)
                yield return pawn.Cell;
    }
}
