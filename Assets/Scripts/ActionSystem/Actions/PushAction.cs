using System.Collections.Generic;

public class PushAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement | ActionTag.Forced;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction() { Formations = TargetableFormation.Self },
            new RangeRestriction(),
            new AdjcentCellContentRestriction() { Content = TargetableCellContent.Empty, Directions = RelativeDirection.Away },
            new CellContentRestriction() { Content = TargetableCellContent.Ally | TargetableCellContent.Enemy }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint()
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new PushNode() { RelativeDirection = RelativeDirection.Away, Distance = 1 }
        };
    }
}
