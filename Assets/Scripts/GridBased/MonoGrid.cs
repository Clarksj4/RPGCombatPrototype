using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MonoGrid : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Occurs when a cell is tapped or clicked on.")]
    private UnityEvent<MonoGrid, Cell> OnCellInput = null;

    private Dictionary<Vector2Int, Cell> cellDirectory = new Dictionary<Vector2Int, Cell>();

    private void Awake()
    {
        foreach (Cell cell in GetComponentsInChildren<Cell>(true))
        {
            cellDirectory.Add(cell.Coordinate, cell);
            cell.OnTapped += HandleOnCellTapped;
        }
    }

    /// <summary>
    /// Checks whether this grid contains the given coordinate.
    /// </summary>
    public bool Contains(Vector2Int coordinate)
    {
        return cellDirectory.ContainsKey(coordinate);
    }
    
    /// <summary>
    /// Gets all the cells on this grid.
    /// </summary>
    public IEnumerable<Cell> GetCells()
    {
        return cellDirectory.Values.Where(c => c.Active);
    }

    /// <summary>
    /// Gets the cell at the given coordinate
    /// </summary>
    public Cell GetCell(Vector2Int coordinate)
    {
        if (cellDirectory.TryGetValue(coordinate, out var cell))
        {
            if (cell.Active)
                return cell;
        }
        
        return null;
    }
    
    /// <summary>
    /// Gets all the cells in the given range.
    /// </summary>
    public IEnumerable<Cell> GetRange(Vector2Int origin, int min, int max)
    {
        for (int x = -max; x <= max; x++)
        {
            int maxY = max - Mathf.Abs(x);
            for (int y = -maxY; y <= maxY; y++)
            {
                int range = Mathf.Abs(x) + Mathf.Abs(y);
                if (range >= min)
                {
                   Vector2Int coordinate = new Vector2Int(origin.x + x, origin.y + y);
                    yield return GetCell(coordinate);
                }
            }
        }
    }

    /// <summary>
    ///  Gets the cell at the given world position
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Cell WorldPositionToCell(Vector3 worldPosition)
    {
        return cellDirectory.Values.FirstOrDefault(c => c.Contains(worldPosition));
    }

    private void HandleOnCellTapped(Cell cell)
    {
        OnCellInput?.Invoke(this, cell);
    }
}
