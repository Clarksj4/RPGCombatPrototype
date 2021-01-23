using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IronStrikeAction : BattleAction
{
    public IronStrikeAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Need to have enough health to cast it
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 0)
        };

        // Can only target self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Enemy),
            new RankCellsRestriction(this, 0)
        };

        // Affects adjacent cells
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Heals and buffs adjacent
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode(this) { Status = new DefenseStatus() { Duration = 1 } },
            new IsHitNode(this),
            new DoDamageNode(this, 20)
        };
    }
}
