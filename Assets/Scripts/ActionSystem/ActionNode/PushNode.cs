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
    public Vector2Int Direction 
    {
        get 
        {
            // Use literal direction
            if (RelativeDirection == RelativeDirection.None)
                return direction;

            // Caluclate relative direction
            return Target.Coordinate.GetRelativeDirections(Actor.Coordinate, RelativeDirection).Single();
        }
        set { direction = value; }
    }
    private Vector2Int direction;

    public override bool Do()
    {
        Pawn pawn = Target.GetContent<Pawn>();
        if (pawn != null)
        {
            pawn.TakePush(Direction, Distance);
            return true;
        }
        
        return false;
    }
}
