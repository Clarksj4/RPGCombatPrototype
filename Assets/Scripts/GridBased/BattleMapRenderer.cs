using UnityEngine;
using UnityEditor;

public class BattleMapRenderer : MonoBehaviour
{
    private BattleMap map;

    private void Awake()
    {
        map = GetComponentInParent<BattleMap>();
    }

    private void OnDrawGizmos()
    {
        if (EditorApplication.isPlaying)
        {
            for (int x = 0; x <= map.Grid.NCells.x; x++)
            {
                Vector2 from = new Vector2(map.Grid.Bounds.min.x + (x * map.Grid.CellSize.x), map.Grid.Bounds.min.y);
                Vector2 to = from + (Vector2.up * map.Grid.CellSize * map.Grid.NCells.y);
                Gizmos.DrawLine(from, to);
            }

            for (int y = 0; y <= map.Grid.NCells.y; y++)
            {
                Vector2 from = new Vector2(map.Grid.Bounds.min.x, map.Grid.Bounds.min.y + (y * map.Grid.CellSize.y));
                Vector2 to = from + (Vector2.right * map.Grid.CellSize * map.Grid.NCells.x);
                Gizmos.DrawLine(from, to);
            }
        }
    }
}
