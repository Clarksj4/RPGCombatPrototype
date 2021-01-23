using System;

[Serializable]
public class EvasiveStatus : PawnStatus
{
    /// <summary>
    /// Gets the number of attacks the afflicted pawn
    /// will evade while affected by this status.
    /// </summary>
    public int AttacksToEvade { get { return attacksToEvade; } }
    private int attacksToEvade;

    public EvasiveStatus(int duration, int attacksToEvade)
        : base(duration)
    {
        this.attacksToEvade = attacksToEvade;
    }

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
        attacksToEvade--;
        if (AttacksToEvade == 0)
            Expire();
    }
}
