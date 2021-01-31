using System.Collections.Generic;

public class BlessAction : BattleAction
{
    protected override void Setup()
    {
        // Need to have enough health to cast it
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 },
            new HealthRestriction() { Amount = 10 }
        };

        // Can only target self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Self }
        };

        // Affects adjacent cells
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRange(this, 1, 1)
        };

        // Costs health
        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 },
            new DoDamageNode() { BaseDamage = 10, Defendable = false, Amplifyable = false }
        };

        // Heals and buffs adjacent
        targetedActions = new List<ActionNode>()
        {
            new HealNode() { Amount = 15 },
            new ApplyStatusNode() { Status = new PowerStatus() { Duration = 1 } }
        };
    }
}
