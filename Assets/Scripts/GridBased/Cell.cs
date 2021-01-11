using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell : MonoBehaviour
{
    /// <summary>
    /// Gets the grid this cell belongs to.
    /// </summary>
    public MonoGrid Parent { get { return GetComponentInParent<MonoGrid>(); } }
    /// <summary>
    /// Gets the contents of this cell.
    /// </summary>
    public IEnumerable<IGridBased> Contents { get { return GetComponentsInChildren<IGridBased>(); } }
    /// <summary>
    /// Gets the position of this cell in world space.
    /// </summary>
    public Vector3 WorldPosition { get { return transform.position; } }
    /// <summary>
    /// Gets the coordinate of this cell.
    /// </summary>
    public Vector2Int Coordinate { get { return coordinate; } }
    /// <summary>
    /// Gets the extents of this cell - equal to half its width and height
    /// </summary>
    public Vector3 Extents { get { return Parent.CellSize / 2; } }
    /// <summary>
    /// Gets whether this cell is currently active.
    /// </summary>
    public bool Active { get { return gameObject.activeSelf; } }
    /// <summary>
    /// Gets the formation that this cell belongs to.
    /// </summary>
    public Formation Formation { get { return FormationManager.Instance.GetFormation(this); } }

    [Tooltip("Whether or not this cell is traversable.")]
    public bool Traversable;
    [SerializeField][HideInInspector]
    private Vector2Int coordinate;

    /// <summary>
    /// Checks whether this cell is occupied by aything.
    /// </summary>
    public bool IsOccupied()
    {
        return Contents.Any(c => c != null);
    }

    /// <summary>
    /// Checks whether this cell is occupied by anything of
    /// the given type.
    /// </summary>
    public bool IsOccupied<T>()
    {
        return Contents.Any(c => c is T && c != null);
    }

    /// <summary>
    /// Gets all the cells neighbouring this one.
    /// </summary>
    public IEnumerable<Cell> GetNeighbours()
    {
        yield return Parent.GetCell(coordinate + Vector2Int.up);
        yield return Parent.GetCell(coordinate + Vector2Int.right);
        yield return Parent.GetCell(coordinate + Vector2Int.down);
        yield return Parent.GetCell(coordinate + Vector2Int.left);
    }

    /// <summary>
    /// Reparents the cell to the given grid and moves it to
    /// the correct position based on its coordinate.
    /// </summary>
    public void Place(MonoGrid parent, Vector2Int coordinate)
    {
        Place(parent, coordinate.x, coordinate.y);
    }

    /// <summary>
    /// Reparents the cell to the given grid and moves it to
    /// the correct position based on its coordinate.
    /// </summary>
    public void Place(MonoGrid parent, int x, int y)
    {
        SetParent(parent);
        SetCoordinate(x, y);
        UpdateName();
    }

    /// <summary>
    /// Updates the position of the cell.
    /// </summary>
    public void UpdatePosition()
    {
        transform.localPosition = -Parent.Extents + (Parent.CellSize * Coordinate) + (Parent.CellSize / 2);
    }

    private void SetParent(MonoGrid parent)
    {
        transform.SetParent(parent.transform, false);
    }

    private void SetCoordinate(Vector2Int coordinate)
    {
        // Set coordinate and update position
        this.coordinate = coordinate;
        UpdatePosition();
    }

    private void SetCoordinate(int x, int y)
    {
        // Set coordinate and update position
        coordinate = new Vector2Int(x, y);
        UpdatePosition();
    }

    private void UpdateName()
    {
        // Also rename it to its coordinate so its easy to
        // identify in the hierarchy
        name = coordinate.ToString();
    }
}
