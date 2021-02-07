using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetingRestrictionDisplayLayout : MonoBehaviour
{
    [Tooltip("The transform that will house all the targeting restriction display items.")]
    [SerializeField] private Transform Content;
    [Tooltip("Prefabs that display targeting restrictions.")]
    [SerializeField] private TargetingRestrictionDisplay[] Prefabs;

    public void Display(IEnumerable<TargetingRestriction> restrictions)
    {
        List<TargetingRestrictionDisplay> children = new List<TargetingRestrictionDisplay>();
        foreach (TargetingRestriction restriction in restrictions)
        {
            // Not all restrictions have a display case
            TargetingRestrictionDisplay display = GetDisplayFor(restriction);
            if (display != null)
            {
                display.transform.SetParent(Content, false);

                // Displays are ordered by defined order.
                int index = GetIndex(display, children);
                display.transform.SetSiblingIndex(index);
                children.Insert(index, display);
            }
        }
    }

    private int GetIndex(TargetingRestrictionDisplay child, List<TargetingRestrictionDisplay> children)
    {
        // Handle null or rempty children
        if (children == null || children.Count == 0)
            return 0;

        // Assumes children list is ordered
        return children.TakeWhile(c => c.Order < child.Order)
                       .Count();
    }

    private TargetingRestrictionDisplay GetDisplayFor(TargetingRestriction restriction)
    {
        // Find corresponding prefab
        TargetingRestrictionDisplay prefab = GetPrefab(restriction);
        if (prefab != null)
        {
            // Set it up and return it
            TargetingRestrictionDisplay instance = Instantiate(prefab);
            instance.Setup(restriction);
            return instance;
        }

        return null;
    }

    private TargetingRestrictionDisplay GetPrefab(TargetingRestriction restriction)
    {
        // Check for a specific display case for this restriction
        TargetingRestrictionDisplay prefab = Prefabs.FirstOrDefault(i => restriction.GetType().Name.Contains(i.DisplaysFor));
        return prefab;
    }
}
