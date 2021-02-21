using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Stat
{
    /// <summary>
    /// Occurs when the value for this stat changes. The change
    /// in value is passed as an argument.
    /// </summary>
    public event Action<Stat, int> OnValueChanged;
    /// <summary>
    /// Occurs when the minimum bounds of this stat changes. The
    /// change in value is passed as an argument.
    /// </summary>
    public event Action<Stat, int> OnMinChanged;
    /// <summary>
    /// Occurs when the maximum bounds of this stat changes. The
    /// change in value is passed as an argument.
    /// </summary>
    public event Action<Stat, int> OnMaxChanged;

    /// <summary>
    /// Gets the name of this stat.
    /// </summary>
    public string Name { get { return name; } }

    /// <summary>
    /// Gets or sets the current value of this stat.
    /// </summary>
    public int Value
    {
        get { return value; }
        set
        {
            int delta = value - this.value;
            this.value = value;
         
            // Only notify listeners if the value actually changed.
            if (delta != 0)
                OnValueChanged?.Invoke(this, delta);
        }
    }

    /// <summary>
    /// Gets or sets the minimum allowable value for this stat.
    /// </summary>
    public int Min
    {
        get { return min; }
        set
        {
            int delta = value - min;
            min = value;

            // Only notify listeners if the value actually changed.
            if (delta != 0)
                OnMinChanged?.Invoke(this, delta);
        }
    }

    /// <summary>
    /// Gets or sets the maximum allowable value for this stat.
    /// </summary>
    public int Max
    {
        get { return max; }
        set
        {
            int delta = value - max;
            max = value;

            // Only notify listeners if the value actually changed.
            if (delta != 0)
                OnMaxChanged?.Invoke(this, delta);
        }
    }

    [Tooltip("The name of this stat - used to identify it.")]
    [SerializeField] private string name;
    [Tooltip("The current value of this stat.")]
    [SerializeField] private int value;
    [Tooltip("The minimum allowable value for this stat.")]
    [SerializeField] private int min;
    [Tooltip("The maximum allowable value for this stat.")]
    [SerializeField] private int max;
}
