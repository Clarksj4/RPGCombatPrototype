using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SummonManager : MonoSingleton<SummonManager>
{
    [SerializeField]
    private Pawn Prefab = null;
    [SerializeField]
    private List<PawnStats> Stats = null;

    public Pawn Spawn(PawnStats stats, Cell cell, int duration)
    {
        if (stats != null)
        {
            // Create new pawn
            Pawn instance = Instantiate(Prefab);
            instance.Stats = stats;
            instance.SetCell(cell);

            // This is the final countdown.
            if (duration > -1)
                instance.AddStatus(new DeathTimerStatus() { Duration = duration });

            return instance;
        }

        return null;
    }

    public Pawn Spawn(string name, Cell cell, int duration)
    {
        PawnStats stats = Stats.FirstOrDefault(p => p.name == name);
        if (stats != null)
            return Spawn(stats, cell, duration);

        return null;
    }
}
