using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class MonoGrid : MonoBehaviour
{
    /// <summary>
    /// Gets the width and height of each cell on this
    /// grid unaffected by scale.
    /// </summary>
    public Vector2 CellSize { get { return cellSize; } }
    /// <summary>
    /// Half the width and height of this grid. 
    /// </summary>
    public Vector2 Extents { get { return grid.Extents; } }

    [SerializeField]
    [Tooltip("The number of cells on each axis of this grid.")]
    private Vector2Int nCells = default;
    [SerializeField]
    [Tooltip("The size of each cell on the grid.")]
    private Vector2 cellSize = default;
    [SerializeField]
    [Tooltip("Occurs when a cell is tapped or clicked on.")]
    private UnityEvent<MonoGrid, Cell> OnCellInput = null;

    private new BoxCollider collider = null;
    private Dictionary<Vector2Int, Cell> cellDirectory = new Dictionary<Vector2Int, Cell>();
    private Grid grid = new Grid();

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        foreach (Cell cell in GetComponentsInChildren<Cell>(true))
            cellDirectory.Add(cell.Coordinate, cell);
    }

    private void OnValidate()
    {
        // Update scaled cell size of grid - it's not a
        // monobehaviour so it doesn't know about scale
        grid.CellSize = cellSize * transform.lossyScale;

        // Sanity check - can't have less than 0 cells
        if (nCells.x < 0) nCells.x = 0;
        if (nCells.y < 0) nCells.y = 0;

        grid.NCells = nCells;

        foreach (Cell cell in GetComponentsInChildren<Cell>())
            cell.UpdatePosition();
    }

    private void OnMouseUpAsButton()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (collider.Raycast(cameraRay, out var hitInfo, 100f))
        {
            Cell cell = WorldPositionToCell(hitInfo.point);
            if (cell != null)
                OnCellInput?.Invoke(this, cell);
        }
    }

    /// <summary>
    /// Checks whether this grid contains the given coordinate.
    /// </summary>
    public bool Contains(Vector2Int coordinate)
    {
        return grid.Contains(coordinate);
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
    public Cell GetCell(Vector3 worldPosition)
    {
        if (WorldPositionToCoordinate(worldPosition, out var coordinate))
            return GetCell(coordinate);

        return null;
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
        return grid.GetRange(origin, min, max).Select(GetCell).Where(c => c != null);
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

    public Cell WorldPositionToCell(Vector3 worldPosition)
    {
        if (WorldPositionToCoordinate(worldPosition, out var coordinate))
            return GetCell(coordinate);

        return null;
    }

    [ContextMenu("MakeCells")]
    public void MakeCells()
    {
        print(grid.Extents);
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
    }
}
