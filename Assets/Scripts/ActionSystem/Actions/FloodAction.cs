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
            new ManaRestriction() { Amount = 3 },
            new RankCellsRestriction() { Ranks = new int[] { 1, 2 } }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction() { Formations = TargetableFormation.Other },
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedFormation(this)
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 3 }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new IsHitNode(),
            new DoDamageNode() { BaseDamage = 30 }
        };
    }
}
