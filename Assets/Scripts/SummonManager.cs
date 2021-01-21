using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SummonManager : MonoSingleton<SummonManager>
{
    [SerializeField]
    private List<Pawn> Pawns = null;

    public Pawn Spawn(string name, Cell cell, float priority, int duration)
    {
        Pawn prefab = Pawns.FirstOrDefault(p => p.name == name);
        if (prefab != null)
        {
            // Create new pawn
            Pawn instance = Instantiate(prefab);
            instance.SetCell(cell);

            // Attack summon countdown
            Summon summon = instance.gameObject.AddComponent<Summon>();
            summon.Setup(duration, priority);

            return instance;
        }

        return null;
    }
}
