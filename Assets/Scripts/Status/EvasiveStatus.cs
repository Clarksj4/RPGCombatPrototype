using System;

[Serializable]
public class EvasiveStatus : PawnStatus
{
    /// <summary>
    /// Gets the number of attacks the afflicted pawn
    /// will evade while affected by this status.
    /// </summary>
    public int AttacksToEvade { get; set; }

    protected override void OnApplication()
    {
        base.OnApplication();

        // Will evade until enough attacks are evaded
        Pawn.Evasive = true;
        Pawn.OnAttacked += Pawn_OnAttacked;
    }

    protected override void OnExpired()
    {
        base.OnExpired();
        Pawn.Evasive = false;
    }

    private void Pawn_OnAttacked(bool obj)
    {
        // Reduce counter - expire if out of evades
        AttacksToEvade--;
        if (AttacksToEvade == 0)
            Expire();
    }
}
