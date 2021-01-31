using System.Collections.Generic;

public class MoveAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction() { Formations = TargetableFormation.Self },
            new RangeRestriction() { Range = Actor.Movement },
            new CellContentRestriction() { Content = TargetableCellContent.Empty }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new MoveNode()
        };
    }
}