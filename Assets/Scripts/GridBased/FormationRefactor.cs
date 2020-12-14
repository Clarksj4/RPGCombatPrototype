using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class FormationRefactor : MonoBehaviour
{
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
    public Vector3 WorldPosition { get { return transform.position; } }
    /// <summary>
    /// The width and height of this formation in world space.
    /// </summary>
    public Vector2 Size { get { return CellSize * NCells; } }
    /// <summary>
    /// Half the width and height of this formation in world space. 
    /// </summary>
    public Vector2 Extents { get { return Size / 2f; } }
    /// <summary>
    /// The local position of the top left corner of this grid.
    /// </summary>
    private Vector3 LocalOriginCorner { get { return new Vector3(-Extents.x, Extents.y); } } 
    /// <summary>
    /// The local position of the top left cell centre.
    /// </summary>
    private Vector3 LocalOriginCellPosition { get { return LocalOriginCorner + HalfCell; } }
    /// <summary>
    /// The size and direction of half a cell.
    /// </summary>
    private Vector3 HalfCell { get { return new Vector3(CellSize.x * 0.5f, -CellSize.y * 0.5f); } }
    /// <summary>
    /// The direction that files increase.
    /// </summary>
    private Vector2Int FileIncrement { get { return Vector2Int.right; } }
    /// <summary>
    /// The direction the ranks increase.
    /// </summary>
    private Vector2Int RankIncrement { get { return Vector2Int.down; } }
    
    [SerializeField]
    [Tooltip("The number of cells on each axis of this grid.")]
    private Vector2Int nCells;
    [SerializeField]
    [Tooltip("The size of each cell on the grid.")]
    private Vector2 cellSize;
    [SerializeField]
    [Tooltip("The collider that determines the edge of this formation.")]
    private new BoxCollider2D collider;

    /// <summary>
    /// Gets the world position of the centre of the cell at the given position.
    /// </summary>
    public Vector3 CoordinateToWorldPosition(Vector2Int coordinate)
    {
        Vector3 scale = new Vector3(coordinate.x * CellSize.x, -coordinate.y * CellSize.y);
        Vector3 localPosition = LocalOriginCellPosition + scale;
        return transform.TransformPoint(localPosition);
    }

    /// <summary>
    /// Gets the coordinate of the cell that contains the given world position.
    /// Returns false if there is no cell that contains the given position.
    /// </summary>
    public bool WorldPositionToCoordinate(Vector3 worldPosition, out Vector2Int coordinate)
    {
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition);

        // Distance from min to position
        Vector3 delta = localPosition - LocalOriginCorner;

        // Continuous coordinate (not necessarily on the grid)
        Vector3 unboundedCoordinate = delta / CellSize;

        // Round it - we don't care about the continuous part - just the
        // coordinate part.
        Vector2Int roundedUnboundedCoordinate = new Vector2Int((int)unboundedCoordinate.x, -(int)unboundedCoordinate.y);

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
    public IEnumerable<Vector2Int> GetRankCoordinates(int rank)
    {
        // How many cells in rank.
        int rankWidth = NCells.x;

        for (int i = 0; i < rankWidth; i++)
        {
            Vector2Int coordinate = FileIncrement * i;
            coordinate.y = rank;
            yield return coordinate;
        }
    }

    /// <summary>
    /// Gets all the coordinates in the given file.
    /// </summary>
    public IEnumerable<Vector2Int> GetFileCoordinates(int file)
    {
        // How many cells in rank.
        int fileDepth = NCells.y;

        for (int i = 0; i < fileDepth; i++)
        {
            Vector2Int coordinate = RankIncrement * i;
            coordinate.x = file;
            yield return coordinate;
        }
    }

    /// <summary>
    /// Gets the coordinate of the cell that is closest to the 
    /// given world position.
    /// </summary>
    public Vector2Int GetClosestCoordinate(Vector2 worldPosition)
    {
        Vector2 closestPoint = collider.ClosestPoint(worldPosition);
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
