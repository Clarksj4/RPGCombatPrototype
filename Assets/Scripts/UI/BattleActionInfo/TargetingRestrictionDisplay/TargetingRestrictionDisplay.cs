using UnityEngine;

public class TargetingRestrictionDisplay : MonoBehaviour
{
    [Header("Layout")]
    [Tooltip("Which targeting restriction class does this object represent?")]
    public string DisplaysFor;
    [Tooltip("Determines in which order this display will be shown in the layout " +
             "compared to other restriction displays.")]
    public int Order;

    /// <summary>
    /// Show iconography and information about the given targeting restriction.
    /// </summary>
    public virtual void Setup(TargetingRestriction restriction) { /* Nothing! */ }
}
