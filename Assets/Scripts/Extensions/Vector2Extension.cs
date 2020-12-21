using UnityEngine;
using System.Collections;

public static class Vector2Extension
{
    /// <summary>
    /// Returns which ever axis of this vector has the greatest
    /// magnitude.
    /// </summary>
    public static float MaxAxisMagnitude(this Vector2 vector)
    {
        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
            return vector.x;
        else
            return vector.y;
    }

    /// <summary>
    /// Discards the lesser of the two axis of this vector.
    /// </summary>
    public static Vector2 MaxAxisOnly(this Vector2 vector)
    {
        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
            return new Vector2(vector.x, 0);
        else
            return new Vector2(0, vector.y);
    }

    /// <summary>
    /// Reduces each dimension of the given coordinate to 1 or 0 whilst
    /// maintaining its positivity or negativity. Useful for getting the
    /// direction a coordinate points without its magnitude.
    /// </summary>
    public static Vector2Int Reduce(this Vector2 vector)
    {
        // If x less / greater than 0, set to 1 but maintain sign.
        int x = 0;
        if (vector.x != 0)
            x = (int)(1 * Mathf.Sign(vector.x));

        // If y less / greater than 0, set to 1 but maintain sign.
        int y = 0;
        if (vector.y != 0)
            y = (int)(1 * Mathf.Sign(vector.y));

        return new Vector2Int(x, y);
    }
}
