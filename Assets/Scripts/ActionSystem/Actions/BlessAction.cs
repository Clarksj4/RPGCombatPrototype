using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlessAction : BattleAction
{
    public override int Range { get { return 1; } }

    public BlessAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Need to have enough health to cast it
        actorRestrictions = new List<TargetingRestriction>()
        {
            new HealthRestriction(this, 10 + 1)
        };

        // Can only target self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Self),
        };

        // Affects adjacent cells
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRange(this, 1, 1)
        };

        // Costs health
        beginningActions = new List<ActionNode>()
        {
            new DoDamageNode(this, 10, true, false) { Target = Actor.Cell }
        };

        // Heals and buffs adjacent
        targetedActions = new List<ActionNode>()
        {
            new HealNode(this) { Amount = 5 },
            new ApplyStatusNode(this) { Status = new PowerStatus() { Duration = 1 } }
        };
    }
}
