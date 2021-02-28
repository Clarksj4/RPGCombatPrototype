using System;

[Serializable]
public class ImmobilizedStatus : PawnStatus
{
    private int removedMovement = 0;

    protected override void OnApplication()
    {
        base.OnApplication();
        removedMovement = Pawn.Stats["Movement"].Value;
        Pawn.Stats["Movement"].Value = 0;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Stats["Movement"].Value += removedMovement;
    }

    public override bool Collate(PawnStatus other)
    {
        // Block agility and evasive statuses
        return other is AgilityStatus || 
               other is EvasiveStatus;
    }
}
