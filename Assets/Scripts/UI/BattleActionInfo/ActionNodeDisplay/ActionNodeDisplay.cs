using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNodeDisplay : MonoBehaviour
{
    [Header("Layout")]
    [Tooltip("Which action node class does this object represent?")]
    public string DisplaysFor;
    [Tooltip("Determines in which order this display will be shown in the layout " +
             "compared to other node displays.")]
    public int Order;

    /// <summary>
    /// Show iconography and information about the given action node.
    /// </summary>
    public virtual void Setup(ActionNode node) { /* Nothing! */ }
}
