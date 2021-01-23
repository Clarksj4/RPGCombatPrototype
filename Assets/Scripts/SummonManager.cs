using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SummonManager : MonoSingleton<SummonManager>
{
    [SerializeField]
    private Pawn Prefab = null;
    [SerializeField]
    private List<PawnStats> Stats = null;

    public Pawn Spawn(string name, Cell cell, float priority, int duration)
    {
        PawnStats stats = Stats.FirstOrDefault(p => p.name == name);
        if (stats != null)
        {
            // Create new pawn
            Pawn instance = Instantiate(Prefab);
            instance.Stats = stats;
            instance.SetCell(cell);

            // Attack summon countdown
            Summon summon = instance.gameObject.AddComponent<Summon>();
            summon.Setup(duration, priority);

            return instance;
        }

        return null;
    }
}
