using UnityEngine;

public static class Vector2IntExtension
{
    /// <summary>
    /// Reduces each dimension of the given coordinate to 1 or 0 whilst
    /// maintaining its positivity or negativity. Useful for getting the
    /// direction a coordinate points without its magnitude.
    /// </summary>
    public static Vector2Int Reduce(this Vector2Int coordinate)
    {
        // If x less / greater than 0, set to 1 but maintain sign.
        int x = 0;
        if (coordinate.x != 0)
            x = coordinate.x / Mathf.Abs(coordinate.x);

        // If y less / greater than 0, set to 1 but maintain sign.
        int y = 0;
        if (coordinate.y != 0)
            y = coordinate.y / Mathf.Abs(coordinate.y);

        return new Vector2Int(x, y);
    }
}
