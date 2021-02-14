using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

public class PushNode : MoveNode
{
    [Tooltip("The distance to push the target.")]
    public int Distance;
    [HideIf("@this.relativeDirection != RelativeDirection.None")]
    [Tooltip("Push in a direction relative to the target's formation.")]
    public FormationMovement FormationDirection = FormationMovement.None;
    [HideIf("@this.FormationDirection != FormationMovement.None")]
    [Tooltip("Push in a direction relative to the caster.")]
    public RelativeDirection relativeDirection = RelativeDirection.None;
    [HideIf("@this.relativeDirection != RelativeDirection.None || this.FormationDirection != FormationMovement.None")]
    [Tooltip("Push in a literal direction.")]
    public Vector2Int Direction;

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
        // Use relative direction
        if (relativeDirection != RelativeDirection.None)
            return target.Coordinate.GetRelativeDirections(actor.Coordinate, relativeDirection).Single();

        // Use formation based direction
        else if (FormationDirection != FormationMovement.None)
            return target.Formation.GetDirection(FormationDirection);

        // Use literal direction
        else
            return Direction;
    }
}
