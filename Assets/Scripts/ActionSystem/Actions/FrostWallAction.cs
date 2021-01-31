using System.Collections.Generic;

public class FrostWallAction : BattleAction
{
    protected override void Setup()
    {
        actorRestrictions = new List<TargetingRestriction>()
        {
            new ManaRestriction() { Amount = 1 }
        };

        // Can only target empty cells on the same rank
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Empty },
            new FormationRestriction() { Formations = TargetableFormation.Self }
        };

        // Affects all cells in targeted rank
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedRank(this)
        };

        // Will only affect empty cells
        areaOfEffectRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction() { Content = TargetableCellContent.Empty }
        };

        selfActions = new List<ActionNode>()
        {
            new RemoveManaNode() { Amount = 1 }
        };

        targetedActions = new List<ActionNode>()
        {
            new SummonNode() { Name = "FrostWall", Priority = Actor.Priority, Duration = 3 }
        };
    }
}
