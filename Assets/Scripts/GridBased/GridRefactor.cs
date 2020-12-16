using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class GridRefactor : MonoBehaviour
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
    private Vector3 LocalOriginCorner { get { return -Extents; } }
    /// <summary>
    /// The local position of the top left cell centre.
    /// </summary>
    private Vector3 LocalOriginCellPosition { get { return LocalOriginCorner + HalfCell; } }
    /// <summary>
    /// The size and direction of half a cell.
    /// </summary>
    private Vector3 HalfCell { get { return new Vector3(CellSize.x * 0.5f, CellSize.y * 0.5f); } }

    [SerializeField]
    [Tooltip("The number of cells on each axis of this grid.")]
    private Vector2Int nCells;
    [SerializeField]
    [Tooltip("The size of each cell on the grid.")]
    private Vector2 cellSize;

    /// <summary>
    /// Gets the world position of the centre of the cell at the given position.
    /// </summary>
    public Vector3 CoordinateToWorldPosition(Vector2Int coordinate)
    {
        Vector3 scale = new Vector3(coordinate.x * CellSize.x, coordinate.y * CellSize.y);
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
        Vector2Int roundedUnboundedCoordinate = new Vector2Int((int)unboundedCoordinate.x, (int)unboundedCoordinate.y);

        coordinate = roundedUnboundedCoordinate;

        // Actually check its on the grid.
        return Contains(coordinate);
    }

    /// <summary>
    /// Checks if the given coordinate is within the bounds of
    /// this grid.
    /// </summary>
    public bool Contains(Vector2Int coordinate)
    {
        return Contains(coordinate.x, coordinate.y);
    }

    /// <summary>
    /// Checks if the given coordinate is within the bounds of
    /// this grid.
    /// </summary>
    public bool Contains(int x, int y)
    {
        return x >= 0 &&
                x <= (NCells.x - 1) &&
                y >= 0 &&
                y <= (NCells.y - 1);
    }

    /// <summary>
    /// Gets all coordinates in the given row on this grid.
    /// </summary>
    public IEnumerable<Vector2Int> GetRowCoordinates(int row)
    {
        // How many cells in rank.
        int rowWidth = NCells.x;

        for (int i = 0; i < rowWidth; i++)
        {
            Vector2Int coordinate = Vector2Int.right * i;
            coordinate.y = row;
            yield return coordinate;
        }
    }

    /// <summary>
    /// Gets all the coordinates in the given file.
    /// </summary>
    public IEnumerable<Vector2Int> GetColumnCoordinates(int column)
    {
        // How many cells in rank.
        int columnHeight = NCells.y;

        for (int i = 0; i < columnHeight; i++)
        {
            Vector2Int coordinate = Vector2Int.up * i;
            coordinate.x = column;
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
