using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class VitalStatistics : ISerializationCallbackReceiver
{
    /// <summary>
    /// Occurs when the value of a stat changes.
    /// </summary>
    public event Action<Stat, int> OnStatValueChanged;
    /// <summary>
    /// Occurs when the minimum allowable value for a stat changes.
    /// </summary>
    public event Action<Stat, int> OnStatMinimumChanged;
    /// <summary>
    /// Occurs when the maximum allowable value for a stat changes.
    /// </summary>
    public event Action<Stat, int> OnStatMaximumChanged;

    /// <summary>
    /// Gets the stat with the given name.
    /// </summary>
    public Stat this[string name] { get { return stats.FirstOrDefault(s => s.Name == name); } }
    /// <summary>
    /// Gets all the status.
    /// </summary>
    public IEnumerable<Stat> Stats { get { return stats; } }

    [SerializeField]
    private Stat[] stats;

    public void OnBeforeSerialize()
    {
        /* Nothing! */
    }

    public void OnAfterDeserialize()
    {
        if (Application.isPlaying)
        {
            foreach (Stat stat in stats)
            {
                stat.OnValueChanged -= HandleOnValueChanged;
                stat.OnValueChanged += HandleOnValueChanged;
                stat.OnMinChanged -= HandleOnMinChanged;
                stat.OnMinChanged += HandleOnMinChanged;
                stat.OnMaxChanged -= HandleOnMaxChanged;
                stat.OnMaxChanged += HandleOnMaxChanged;
            }
        }
    }

    private void HandleOnMaxChanged(Stat stat, int delta)
    {
        OnStatMaximumChanged?.Invoke(stat, delta);
    }

    private void HandleOnMinChanged(Stat stat, int delta)
    {
        OnStatMinimumChanged?.Invoke(stat, delta);
    }

    private void HandleOnValueChanged(Stat stat, int delta)
    {
        OnStatValueChanged?.Invoke(stat, delta);
    }
}
