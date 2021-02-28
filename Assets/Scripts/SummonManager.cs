using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SummonManager : MonoSingleton<SummonManager>
{
    [SerializeField]
    private Pawn Prefab = null;
    [SerializeField]
    private List<PawnData> Stats = null;

    public Pawn Spawn(PawnData data, Cell cell, int duration)
    {
        if (data != null)
        {
            // Create new pawn
            Pawn instance = Instantiate(Prefab);
            instance.Data = data;
            instance.SetCell(cell);

            // This is the final countdown.
            if (duration > -1)
                instance.Statuses.Add(new DeathTimerStatus() { Duration = duration });

            return instance;
        }

        return null;
    }

    public Pawn Spawn(string name, Cell cell, int duration)
    {
        PawnData stats = Stats.FirstOrDefault(p => p.name == name);
        if (stats != null)
            return Spawn(stats, cell, duration);

        return null;
    }
}
