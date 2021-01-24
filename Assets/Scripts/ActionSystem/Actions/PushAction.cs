using System.Collections.Generic;

public class PushAction : BattleAction
{
    public override int Range { get { return 1; } }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Forced;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction() { Actor = Actor, Formations = TargetableFormation.Self },
            new RangeRestriction() { Actor = Actor },
            new AdjcentCellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Empty, Directions = RelativeDirection.Away },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Ally | TargetableCellContent.Enemy }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new PushNode() { Actor = Actor, RelativeDirection = RelativeDirection.Away, Distance = Range }
        };
    }
}
