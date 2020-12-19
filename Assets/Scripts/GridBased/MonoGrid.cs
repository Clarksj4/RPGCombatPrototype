﻿using UnityEngine;

[ExecuteInEditMode]
public class MonoGrid : MonoBehaviour
{
    /// <summary>
    /// Gets the number of cells in each column and row
    /// on this grid.
    /// </summary>
    public Vector2Int NCells { get { return nCells; } } 
    /// <summary>
    /// Gets the width and height of each cell on this
    /// grid unaffected by scale.
    /// </summary>
    public Vector2 CellSize { get { return cellSize; } }
    /// <summary>
    /// Half the width and height of this formation in world space. 
    /// </summary>
    public Vector2 Extents { get { return grid.Extents; } }

    [SerializeField]
    [Tooltip("The number of cells on each axis of this grid.")]
    private Vector2Int nCells;
    [SerializeField]
    [Tooltip("The size of each cell on the grid.")]
    private Vector2 cellSize;

    private Cell[,] cells = new Cell[0,0];
    private GridRefactor grid = new GridRefactor();

    private void OnValidate()
    {
        // Update scaled cell size of grid - it's not a
        // monobehaviour so it doesn't know about scale
        grid.CellSize = cellSize * transform.lossyScale;

        // Sanity check - can't have less than 0 cells
        if (nCells.x < 0) nCells.x = 0;
        if (nCells.y < 0) nCells.y = 0;

        // Resize array of cells
        if (nCells != grid.NCells)
        {
            grid.NCells = nCells;
            cells.ResizeArray(nCells.x, nCells.y);
        }

        else
        {
            foreach (Cell cell in cells)
                cell.UpdatePosition();
        }
    }

    /// <summary>
    /// Gets the world position of the centre of the cell at the given position.
    /// </summary>
    public Vector3 CoordinateToWorldPosition(Vector2Int coordinate)
    {
        Vector3 localPosition = grid.CoordinateToPosition(coordinate);
        return transform.TransformPoint(localPosition);
    }

    /// <summary>
    /// Gets the coordinate of the cell that contains the given world position.
    /// Returns false if there is no cell that contains the given position.
    /// </summary>
    public bool WorldPositionToCoordinate(Vector3 worldPosition, out Vector2Int coordinate)
    {
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
        bool contains = grid.PositionToCoordinate(localPosition, out coordinate);
        return contains;
    }

    [ContextMenu("MakeCells")]
    public void MakeCells()
    {
        cells = new Cell[nCells.x, nCells.y];
        for (int x = 0; x < nCells.x; x++)
        {
            for (int y = 0; y < nCells.y; y++)
                MakeCell(x, y);
        }
    }

    private void MakeCell(int x, int y)
    {
        GameObject gameObject = new GameObject();
        Cell cell = gameObject.AddComponent<Cell>();
        cell.Place(this, x, y);
        cells[x, y] = cell;
    }
}
