using System.Collections.Generic;

public class PierceAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Only usable from the front rank
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 0 } }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FileCellsRestriction() { Actor = Actor, File = Actor.File },
            new ExposedCellsRestriction() { Actor = Actor },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Enemy }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedFile(this, Actor.File)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 25 }
        };

        // If one of the targets is not hit, then no subsequent targets are hit
        AffectedCellsIndependent = false;
    }
}
