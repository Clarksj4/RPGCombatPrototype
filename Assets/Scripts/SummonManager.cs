using System.Collections.Generic;
using System.Linq;

public class SummonManager : MonoSingleton<SummonManager>
{
    public List<Summon> Summons;

    public Pawn Spawn(string name, Cell cell, float priority, int duration)
    {
        Summon prefab = Summons.FirstOrDefault(p => p.name == name);
        if (prefab != null)
        {
            Summon instance = Instantiate(prefab);
            instance.SetCell(cell);
            instance.Setup(priority, duration);
            return instance;
        }

        return null;
    }
}
