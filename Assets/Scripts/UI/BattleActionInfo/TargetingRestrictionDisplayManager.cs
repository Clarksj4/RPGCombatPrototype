using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetingRestrictionDisplayManager : MonoSingleton<TargetingRestrictionDisplayManager>
{
    public TargetingRestrictionIcon DefaultPrefab;
    public TargetingRestrictionIcon[] SpecificCases;

    public TargetingRestrictionIcon GetDisplayFor(TargetingRestriction restriction)
    {
        // Find corresponding prefab
        TargetingRestrictionIcon prefab = GetPrefab(restriction);
        
        // Set it up and return it
        TargetingRestrictionIcon instance = Instantiate(prefab);
        instance.Setup(restriction);
        return instance;
    }

    private TargetingRestrictionIcon GetPrefab(TargetingRestriction restriction)
    {
        // Check for a specific display case for this restriction
        TargetingRestrictionIcon prefab = SpecificCases.FirstOrDefault(i => i.DisplaysFor == restriction.GetType().Name);

        // Nothing specific, use generic case.
        if (prefab == null)
            prefab = DefaultPrefab;

        return prefab;
    }
}
