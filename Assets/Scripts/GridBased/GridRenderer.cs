using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Grid))]
public class GridRenderer : MonoBehaviour
{
    private Grid grid;

    private void Awake()
    {
        grid = GetComponentInParent<Grid>();
    }

    private void OnDrawGizmos()
    {
        if (EditorApplication.isPlaying)
        {
            for (int x = 0; x <= grid.NCells.x; x++)
            {
                Vector2 from = new Vector2(grid.Bounds.min.x + (x * grid.CellSize.x), grid.Bounds.min.y);
                Vector2 to = from + (Vector2.up * grid.CellSize * grid.NCells.y);
                Gizmos.DrawLine(from, to);
            }

            for (int y = 0; y <= grid.NCells.y; y++)
            {
                Vector2 from = new Vector2(grid.Bounds.min.x, grid.Bounds.min.y + (y * grid.CellSize.y));
                Vector2 to = from + (Vector2.right * grid.CellSize * grid.NCells.x);
                Gizmos.DrawLine(from, to);
            }
        }
    }
}
