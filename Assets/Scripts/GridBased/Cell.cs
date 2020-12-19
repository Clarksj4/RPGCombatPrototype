using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public MonoGrid Parent { get { return GetComponentInParent<MonoGrid>(); } }
    public IEnumerable<IGridBased> Contents { get { return GetComponentsInChildren<IGridBased>(); } }
    public Vector2Int Coordinate { get { return coordinate; } }
    public Vector3 Extents { get { return Parent.CellSize / 2; } }

    public bool Passable;
    [SerializeField][HideInInspector]
    private Vector2Int coordinate;

    private void OnDrawGizmos()
    {
        Vector3 corner1 = transform.TransformPoint(new Vector3(-Extents.x, -Extents.y));
        Vector3 corner2 = transform.TransformPoint(new Vector3(-Extents.x, Extents.y));
        Vector3 corner3 = transform.TransformPoint(new Vector3(Extents.x, Extents.y));
        Vector3 corner4 = transform.TransformPoint(new Vector3(Extents.x, -Extents.y));

        Gizmos.DrawLine(corner1, corner2);
        Gizmos.DrawLine(corner2, corner3);
        Gizmos.DrawLine(corner3, corner4);
        Gizmos.DrawLine(corner4, corner1);
    }

    public void Place(MonoGrid parent, Vector2Int coordinate)
    {
        Place(parent, coordinate.x, coordinate.y);
    }
    public void Place(MonoGrid parent, int x, int y)
    {
        SetParent(parent);
        SetCoordinate(x, y);
        UpdateName();
    }

    public void SetParent(MonoGrid parent)
    {
        transform.SetParent(parent.transform, false);
    }

    public void SetCoordinate(Vector2Int coordinate)
    {
        // Set coordinate and update position
        this.coordinate = coordinate;
        UpdatePosition();
    }

    public void SetCoordinate(int x, int y)
    {
        // Set coordinate and update position
        coordinate = new Vector2Int(x, y);
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        transform.localPosition = -Parent.Extents + (Parent.CellSize * Coordinate) + (Parent.CellSize / 2);
    }

    private void UpdateName()
    {
        // Also rename it to its coordinate so its easy to
        // identify in the hierarchy
        name = coordinate.ToString();
    }
}
