using UnityEngine;
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
    /// Gets or sets the current value of this stat.
    /// </summary>
    public int Value
    {
        get { return value; }
        set
        {
            // Clamp value change so it doesn't exceed min or max.
            int oldValue = this.value;
            this.value = Mathf.Clamp(value, min, max);
            int delta = value - oldValue;
         
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
    public string Name;
    [Tooltip("The current value of this stat.")]
    [SerializeField] private int value = 0;
    [Tooltip("The minimum allowable value for this stat.")]
    [SerializeField] private int min = 0;
    [Tooltip("The maximum allowable value for this stat.")]
    [SerializeField] private int max = 100;

    /// <summary>
    /// Checks if this stat currently has a value that is not 0.
    /// </summary>
    public bool HasValue()
    {
        return Value != 0;
    }

    /// <summary>
    /// Increases this stat's value by the given amount.
    /// </summary>
    public void Increment(int increment)
    {
        Value += increment;
    }

    /// <summary>
    /// Decreases this stat's value by the given amount.
    /// </summary>
    public void Decrement(int decrement)
    {
        Value -= decrement;
    }
}
