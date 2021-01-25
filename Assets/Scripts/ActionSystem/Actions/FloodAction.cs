using System.Collections.Generic;

public class FloodAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Only usable when not in front rank
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction() { Actor = Actor, Ranks = new int[] { 1, 2 } }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction() { Actor = Actor, Formations = TargetableFormation.Other },
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedFormation(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 15 }
        };
    }
}
