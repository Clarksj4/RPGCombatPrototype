using System.Collections.Generic;
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

    /// <summary>
    /// Gets a vector that is perpendicular to this one.
    /// </summary>
    public static Vector2Int Perpendicular(this Vector2Int coordinate)
    {
        return new Vector2Int(coordinate.y, coordinate.x);
    }

    /// <summary>
    /// Gets a collection of direction vectors pointing in the
    /// given directions relative to the given coordainte.
    /// </summary>
    public static IEnumerable<Vector2Int> GetRelativeDirections(this Vector2Int coordinate, Vector2Int from, RelativeDirection direction)
    {
        if (direction.HasFlag(RelativeDirection.Away))
            yield return coordinate.Away(from);
        if (direction.HasFlag(RelativeDirection.Towards))
            yield return coordinate.Towards(from);
        if (direction.HasFlag(RelativeDirection.Left))
            yield return coordinate.Left(from);
        if (direction.HasFlag(RelativeDirection.Right))
            yield return coordinate.Right(from);
    }

    /// <summary>
    /// Gets a direction vector pointing towards the target.
    /// </summary>
    public static Vector2Int Towards(this Vector2Int origin, Vector2Int target)
    {
        return (target - origin).Reduce();
    }

    /// <summary>
    /// Gets a direction vector pointing away from the target.
    /// </summary>
    public static Vector2Int Away(this Vector2Int origin, Vector2Int target)
    {
        return (origin - target).Reduce();
    }

    /// <summary>
    /// Gets a direction vector pointing to the left of the target.
    /// </summary>
    public static Vector2Int Left(this Vector2Int origin, Vector2Int target)
    {
        return Away(origin, target).Perpendicular();
    }

    /// <summary>
    /// Gets a direction vector pointing to the right of the target.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Vector2Int Right(this Vector2Int origin, Vector2Int target)
    {
        return Towards(origin, target).Perpendicular();
    }

    /// <summary>
    /// Returns which ever axis of this vector has the greatest
    /// magnitude.
    /// </summary>
    public static int MaxAxisMagnitude(this Vector2Int coordinate)
    {
        if (Mathf.Abs(coordinate.x) > Mathf.Abs(coordinate.y))
            return coordinate.x;
        else
            return coordinate.y;
    }

    /// <summary>
    /// Gets the distance in steps between the two coordinates.
    /// </summary>
    public static int GetTravelDistance(this Vector2Int from, Vector2Int to)
    {
        Vector2Int delta = to - from;
        return Mathf.Abs(delta.x) + Mathf.Abs(delta.y);
    }

    /// <summary>
    /// Returns the absolute value of each axis of this vector.
    /// </summary>
    public static Vector2Int Abs(this Vector2Int vector)
    {
        return new Vector2Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }
}
