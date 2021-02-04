using UnityEngine;

public abstract class TargetingRestrictionIcon : MonoBehaviour
{
    public string DisplaysFor;

    public abstract void Setup(TargetingRestriction restriction);
}
