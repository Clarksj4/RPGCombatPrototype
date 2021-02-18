using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BattleActionElementDisplayManager : MonoSingleton<BattleActionElementDisplayManager>
{
    [Tooltip("Collection of display elements for showing concise units of information about a battle action.")]
    public List<BattleActionElementDisplay> Prefabs;

    public BattleActionElementDisplay GetDisplay(IBattleActionElement element)
    {
        // If there is a display for the given element, make it!
        BattleActionElementDisplay prefab = GetPrefab(element);
        if (prefab != null)
            return Instantiate(prefab);
        
        // No applicable display - give em nutin!
        return null;
    }

    private BattleActionElementDisplay GetPrefab(IBattleActionElement element)
    {
        return Prefabs.FirstOrDefault(p => p.DisplaysFor == element.name);
    }
}
