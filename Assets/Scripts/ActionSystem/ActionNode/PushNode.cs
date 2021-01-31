using System.Linq;
using UnityEngine;

public class PushNode : MoveNode
{
    /// <summary>
    /// Gets or sets the distance to push the contents of the targeted cell.
    /// </summary>
    public int Distance { get; set; }
    /// <summary>
    /// Gets or sets the direction to push the targeted cell, relative to
    /// the origin of the action.
    /// </summary>
    public RelativeDirection RelativeDirection { get; set; }
    /// <summary>
    /// Gets or sets the direction the contents of the targeted cell will
    /// be pushed.
    /// </summary>
    public Vector2Int Direction { get; set; }

    public override bool Do(Pawn actor, Cell target)
    {
        Pawn pawn = target.GetContent<Pawn>();
        if (pawn != null)
        {
            pawn.TakePush(GetDirection(actor, target), Distance);
            return true;
        }
        
        return false;
    }

    private Vector2Int GetDirection(Pawn actor, Cell target)
    {
        // Use literal direction
        if (RelativeDirection == RelativeDirection.None)
            return Direction;

        // Caluclate relative direction
        return target.Coordinate.GetRelativeDirections(actor.Coordinate, RelativeDirection).Single();
    }
}
