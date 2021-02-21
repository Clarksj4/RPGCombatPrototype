using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cell : MonoBehaviour
{
    public event Action<Cell> OnTapped;

    /// <summary>
    /// Gets the grid this cell belongs to.
    /// </summary>
    public MonoGrid Grid { get { return GetComponentInParent<MonoGrid>(); } }
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
    /// Gets whether this cell is currently active.
    /// </summary>
    public bool Active { get { return gameObject.activeSelf; } }
    /// <summary>
    /// Gets the formation that this cell belongs to.
    /// </summary>
    public Formation Formation { get { return FormationManager.Instance.GetFormation(this); } }
    /// <summary>
    /// Half the size of this cell.
    /// </summary>
    private Vector2 Extents { get { return Size / 2; } }

    [Tooltip("Whether or not this cell is traversable.")]
    public bool Traversable;
    [SerializeField]
    private Vector2Int coordinate;
    [SerializeField]
    private Vector2 Size;

    private new BoxCollider collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnMouseUpAsButton()
    {
        OnTapped?.Invoke(this);
    }

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
    /// Gets the first item in the cell that is of the given type.
    /// </summary>
    public T GetContent<T>() where T : IGridBased
    {
        IGridBased content = Contents.FirstOrDefault(c => c is T);
        if (content != null)
            return (T)content;
        return default;
    }

    /// <summary>
    /// Gets all the cells neighbouring this one.
    /// </summary>
    public IEnumerable<Cell> GetNeighbours()
    {
        yield return Grid.GetCell(coordinate + Vector2Int.up);
        yield return Grid.GetCell(coordinate + Vector2Int.right);
        yield return Grid.GetCell(coordinate + Vector2Int.down);
        yield return Grid.GetCell(coordinate + Vector2Int.left);
    }

    /// <summary>
    /// Is the given world position within the bounds of this cell?
    /// </summary>
    public bool Contains(Vector3 worldPosition)
    {
        
        Vector3 localPoint = transform.InverseTransformPoint(worldPosition);
        return localPoint.x < Mathf.Abs(Extents.x) &&
                localPoint.y < Mathf.Abs(Extents.y);
    }
}
