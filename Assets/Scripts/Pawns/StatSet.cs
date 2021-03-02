using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class StatSet : MonoBehaviour
{
    /// <summary>
    /// Occurs when the value of a stat changes.
    /// </summary>
    public UnityEvent<Stat, int> OnStatValueChanged;
    /// <summary>
    /// Occurs when the minimum allowable value for a stat changes.
    /// </summary>
    public UnityEvent<Stat, int> OnStatMinimumChanged;
    /// <summary>
    /// Occurs when the maximum allowable value for a stat changes.
    /// </summary>
    public UnityEvent<Stat, int> OnStatMaximumChanged;

    /// <summary>
    /// Gets the stat with the given name.
    /// </summary>
    public Stat this[string name] 
    { 
        get 
        {
            // If the stat being asked for doesn't exist - make it!
            Stat requested = Stats.FirstOrDefault(s => s.Name == name);
            if (requested == null)
                requested = Add(name);
            return requested;
        } 
    }
    /// <summary>
    /// Gets all the stats.
    /// </summary>
    public List<Stat> Stats { get; private set; } = new List<Stat>();

    // Components
    private Pawn pawn;

    private void Awake()
    {
        pawn = GetComponent<Pawn>();
    }

    /// <summary>
    /// Adds the given stat to this collection of stats.
    /// </summary>
    public Stat Add(Stat stat)
    {
        Stats.Add(stat);
        stat.OnValueChanged -= HandleOnValueChanged;
        stat.OnValueChanged += HandleOnValueChanged;
        stat.OnMinChanged -= HandleOnMinChanged;
        stat.OnMinChanged += HandleOnMinChanged;
        stat.OnMaxChanged -= HandleOnMaxChanged;
        stat.OnMaxChanged += HandleOnMaxChanged;

        return stat;
    }

    /// <summary>
    /// Adds a new stat with the given name
    /// </summary>
    public Stat Add(string name)
    {
        Stat stat = new Stat();
        stat.Name = name;

        return Add(stat);
    }

    /// <summary>
    /// Checks if a stat with the given name exists and contains
    /// a value other than 0.
    /// </summary>
    public bool HasValue(string name)
    {
        Stat stat = Stats.FirstOrDefault(s => s.Name == name);
        if (stat != null)
            return stat.HasValue();
        return false;
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
