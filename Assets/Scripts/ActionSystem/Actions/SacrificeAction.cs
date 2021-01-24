using System.Collections.Generic;

public class SacrificeAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Heal;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new HealthRestriction() { Actor = Actor, Amount = 20 }
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Ally },
            new HealthRestriction() { Actor = Actor, Amount = -1 }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        beginningActions = new List<ActionNode>()
        {
            new DoDamageNode() { Actor = Actor, BaseDamage = 20, Target = Actor.Cell }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new HealNode() { Actor = Actor, Amount = 20 }
        };
    }
}
