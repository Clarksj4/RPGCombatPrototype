using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Grid : MonoBehaviour
{
    /// <summary>
    /// Gets the cell on the flank of the font of this grid. 
    /// </summary>
    public Vector2Int FrontFlankCoordinate { get { return frontFlankCoordinate; } }
    /// <summary>
    /// Gets the direction to the front rank of the grid.
    /// </summary>
    public Vector2Int Forward { get { return forward; } }
    /// <summary>
    /// Gets the number of cells on each axis of this grid.
    /// </summary>
    public Vector2Int NCells { get { return nCells; } }
    /// <summary>
    /// Gets the size of the cells on this grid - adjusted
    /// by the scale of this components transform.
    /// </summary>
    public Vector2 CellSize { get { return Vector2.Scale(cellSize, transform.localScale); } }
    /// <summary>
    /// Gets the world position of this grid (same as
    /// transform.position).
    /// </summary>
    public Vector2 WorldPosition { get { return transform.position; } }
    /// <summary>
    /// Gets the world space bounds of this grid.
    /// </summary>
    public Bounds Bounds { get { return new Bounds(WorldPosition, nCells * CellSize); } }

    // TODO: maybe use FRONT instead of FORWARD?
    // TODO: OR maybe it DID make more sense that the FRONT was determined relative to the position of ANOTHER formation
    [SerializeField][Tooltip("A cell on the flank of the font of this grid.")]
    private Vector2Int frontFlankCoordinate;
    [SerializeField][Tooltip("The direction this grid is facing.")]
    private Vector2Int forward;
    [SerializeField][Tooltip("The number of cells on each axis of this grid.")]
    private Vector2Int nCells;
    [SerializeField][Tooltip("The size of each cell on the grid.")]
    private Vector2 cellSize;

    /// <summary>
    /// Gets the world position of the centre of the cell at the given position.
    /// </summary>
    public Vector2 CoordinateToWorldPosition(Vector2Int coordinate)
    {
        return (Vector2)Bounds.min + (coordinate * CellSize) + (CellSize * 0.5f);
    }

    /// <summary>
    /// Gets the coordinate of the cell that contains the given world position.
    /// Returns false if there is no cell that contains the given position.
    /// </summary>
    public bool WorldPositionToCoordinate(Vector2 worldPosition, out Vector2Int coordinate)
    {
        // Distance from min to position
        Vector2 delta = worldPosition - (Vector2)Bounds.min;

        // Continuous coordinate (not necessarily on the grid)
        Vector2 unboundedCoordinate = delta / CellSize;

        // Round it - we don't care about the continuous part - just the
        // coordinate part.
        Vector2Int roundedUnboundedCoordinate = new Vector2Int((int)unboundedCoordinate.x, (int)unboundedCoordinate.y);

        coordinate = roundedUnboundedCoordinate;

        // Actually check its on the grid.
        return ContainsCoordinate(coordinate);
    }

    /// <summary>
    /// Checks if the given coordinate is within the bounds of
    /// this grid.
    /// </summary>
    public bool ContainsCoordinate(Vector2Int coordinate)
    {
        return ContainsCoordinate(coordinate.x, coordinate.y);
    }

    /// <summary>
    /// Checks if the given coordinate is within the bounds of
    /// this grid.
    /// </summary>
    public bool ContainsCoordinate(int x, int y)
    {
        return x >= 0 &&
                x <= (NCells.x - 1) &&
                y >= 0 &&
                y <= (NCells.y - 1);
    }

    /// <summary>
    /// Gets a collection of all coordinates with the given
    /// range of the given coordinate.
    /// </summary>
    public IEnumerable<Vector2Int> GetCoordinatesInRange(Vector2Int origin, int range)
    {
        for (int i = -range; i <= range; i++)
        {
            int x = origin.x + i;
            
            // Get Y coordinate such that it doesn't exceed 
            // the given range with the current x coordinate.
            int startY = origin.y - (range - Math.Abs(i));
            int endY = origin.y + (range - Math.Abs(i));

            for (int y = startY; y <= endY; y++)
            {
                if (ContainsCoordinate(x, y))
                    yield return new Vector2Int(x, y);
            }
        }
    }
    
    /// <summary>
    /// Checks if the two coordinates are within min and max range of
    /// each other.
    /// </summary>
    public bool IsInRange(Vector2Int from, Vector2Int to, int maxRange, int minRange = 0)
    {
        int distance = GetDistance(from, to);
        return distance <= maxRange && distance >= minRange;
    }

    /// <summary>
    /// Gets the distance in steps between the two coordinates.
    /// </summary>
    public int GetDistance(Vector2Int from, Vector2Int to)
    {
        Vector2Int delta = to - from;
        return Mathf.Abs(delta.x) + Mathf.Abs(delta.y);
    }
    
    /// <summary>
    /// Gets the coordinates of the cells in the front rank
    /// of this grid.
    /// </summary>
    public IEnumerable<Vector2Int> GetFrontRankCoordinates()
    {
        return GetRankCoordinates(0);
    }

    /// <summary>
    /// Gets all coordinates in the given rank on this grid.
    /// </summary>
    public IEnumerable <Vector2Int> GetRankCoordinates(int rank)
    {
        // First coordinate in rank
        Vector2Int rankFlank = FrontFlankCoordinate + (-Forward * rank);
        Debug.Log($"rankFlank: {rankFlank}");

        // The direction to step when counting coordinates in rank.
        Vector2Int step = Forward.Perpendicular();
        Debug.Log($"step: {step}");

        // How many cells in rank.
        int rankWidth = GetRankWidth();
        Debug.Log($"rankWidth: {rankWidth}");

        for (int i = 0; i < rankWidth; i++)
        {
            Vector2Int coordinate = rankFlank + (step * i);
            Debug.Log($"coordinate: {coordinate}");
            yield return coordinate;
        }
    }

    /// <summary>
    /// Gets the rank of the given coordinate.
    /// </summary>
    public int GetRank(Vector2Int coordinate)
    {
        Vector2Int deltaVector = FrontFlankCoordinate - coordinate;
        int deltaRank = Vector2Int.Scale(deltaVector, Forward).MaxAxisMagnitude();
        int rank = Mathf.Abs(deltaRank);
        return rank;
    }

    /// <summary>
    /// Gets the number of ranks deep the theis grid runs.
    /// </summary>
    public int GetNRanks()
    {
        Vector2Int direction = Vector2Int.Scale(Forward, NCells);
        return Mathf.Abs(direction.MaxAxisMagnitude());
    }

    /// <summary>
    /// Gets the width of ranks on this grid.
    /// </summary>
    public int GetRankWidth()
    {
        Vector2Int step = Forward.Perpendicular();

        Vector2Int fullStep = Vector2Int.Scale(step, NCells);
        int rankWidth = Mathf.Abs(fullStep.MaxAxisMagnitude());
        return rankWidth;
    }

    /// <summary>
    /// Gets the coordinate of the cell that is closest to the 
    /// given world position.
    /// </summary>
    public Vector2Int GetClosestCoordinate(Vector2 worldPosition)
    {
        Vector2 closestPoint = Bounds.ClosestPoint(worldPosition);
        bool onGrid = WorldPositionToCoordinate(closestPoint, out Vector2Int closestCoordinate);
        return closestCoordinate;
    }

    /// <summary>
    /// Returns all the coordinates on this grid in a line
    /// beginning at origin, and proceeding in the given
    /// direction with the given magnitude.
    /// </summary>
    public IEnumerable<Vector2Int> GetCoordinatesInLine(Vector2Int origin, Vector2Int line)
    {
        int n = Mathf.Max(Mathf.Abs(line.x), Mathf.Abs(line.y));
        Vector2Int step = line.Reduce();
        for (int i = 0; i <= n; i++)
        {
            Vector2Int coordinate = origin + (step * i);
            if (ContainsCoordinate(coordinate))
                yield return coordinate;
        }
    }

    /// <summary>
    /// Gets all the coordinates on this grid.
    /// </summary>
    public IEnumerable<Vector2Int> GetCoordinates()
    {
        for (int x = 0; x < NCells.x; x++)
        {
            for (int y = 0; y < NCells.y; y++)
                yield return new Vector2Int(x, y);
        }
    }
}
