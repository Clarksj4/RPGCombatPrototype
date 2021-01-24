using System.Collections.Generic;

public class FlameWaveAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Only usable from front rank
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 0 } }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction() { Actor = Actor, Formations = TargetableFormation.Other },
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 0 } }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRank(this, 0)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 20 }
        };
    }
}
