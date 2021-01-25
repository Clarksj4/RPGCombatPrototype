using System.Collections.Generic;

public class BlessAction : BattleAction
{
    protected override void Setup()
    {
        // Need to have enough health to cast it
        actorRestrictions = new List<TargetingRestriction>()
        {
            new HealthRestriction() { Actor = Actor, Amount = 10 }
        };

        // Can only target self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Self }
        };

        // Affects adjacent cells
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRange(this, 1, 1)
        };

        // Costs health
        beginningActions = new List<ActionNode>()
        {
            new DoDamageNode() 
            { 
                Actor = Actor, 
                BaseDamage = 10, 
                Defendable = false, 
                Amplifyable = false, 
                Target = Actor.Cell 
            }
        };

        // Heals and buffs adjacent
        targetedActions = new List<ActionNode>()
        {
            new HealNode() { Actor = Actor, Amount = 15 },
            new ApplyStatusNode() { Actor = Actor, Status = new PowerStatus() { Duration = 1 } }
        };
    }
}
