using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IGridInputHandle
{
    void OnGridInput(Grid grid, Vector2Int coordinate);
}

public class Grid : MonoBehaviour
{
    public Vector2Int nCells = Vector2Int.one * 3;
    public Vector2 cellSize = Vector2.one;

    private void OnDrawGizmos()
    {
        for (int x = 0; x <= nCells.x; x++)
        {
            Vector2 from = new Vector2(x * cellSize.x, 0);
            Vector2 to = from + (Vector2.up * cellSize * nCells.y);
            Gizmos.DrawLine(from, to);
        }

        for (int y = 0; y <= nCells.y; y++)
        {
            Vector2 from = new Vector2(0, y * cellSize.y);
            Vector2 to = from + (Vector2.right * cellSize * nCells.x);
            Gizmos.DrawLine(from, to);
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (WorldPositionToCoordinate(mousePosition, out var coordinate))
            NotifyHandler(coordinate);
    }

    public Vector2 CoordinateToWorldPosition(Vector2Int coordinate)
    {
        return (Vector2)transform.position + (coordinate * cellSize) + (cellSize * 0.5f);
    }

    public bool WorldPositionToCoordinate(Vector2 worldPosition, out Vector2Int coordinate)
    {
        Vector2 delta = worldPosition - (Vector2)transform.position;
        Vector2 unboundedCoordinate = delta / cellSize;
        Vector2Int roundedUnboundedCoordinate = new Vector2Int((int)unboundedCoordinate.x, (int)unboundedCoordinate.y);

        coordinate = roundedUnboundedCoordinate;

        return ContainsCoordinate(coordinate);
    }

    public bool ContainsCoordinate(Vector2Int coordinate)
    {
        return coordinate.x >= 0 &&
                coordinate.x <= (nCells.x - 1) &&
                coordinate.y >= 0 &&
                coordinate.y <= (nCells.y - 1);
    }

    private void NotifyHandler(Vector2Int coordinate)
    {
        IGridInputHandle handler = GetComponent<IGridInputHandle>();
        if (handler == null)
            handler = GetComponentInParent<IGridInputHandle>();

        handler?.OnGridInput(this, coordinate);
    }
}
