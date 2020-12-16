using UnityEngine;
using System.Collections;
using UnityEditor;

[RequireComponent(typeof(GridRefactor))]
public class GridRefactorRenderer : MonoBehaviour
{
    private Vector3 UnrotatedCorner1 { get { return new Vector3(-formation.Extents.x, formation.Extents.y); } }
    private Vector3 UnrotatedCorner2 { get { return new Vector3(-formation.Extents.x, -formation.Extents.y); } }
    private Vector3 UnrotatedCorner3 { get { return new Vector3(formation.Extents.x, -formation.Extents.y); } }
    private Vector3 UnrotatedCorner4 { get { return new Vector3(formation.Extents.x, formation.Extents.y); } }
    private Vector3 Corner1 { get { return transform.TransformPoint(UnrotatedCorner1); } }
    private Vector3 Corner2 { get { return transform.TransformPoint(UnrotatedCorner2); } }
    private Vector3 Corner3 { get { return transform.TransformPoint(UnrotatedCorner3); } }
    private Vector3 Corner4 { get { return transform.TransformPoint(UnrotatedCorner4); } }

    [SerializeField]
    private GridRefactor formation;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(Corner1, Corner2);
        Gizmos.DrawLine(Corner2, Corner3);
        Gizmos.DrawLine(Corner3, Corner4);
        Gizmos.DrawLine(Corner4, Corner1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Corner1, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Corner3, 0.1f);

        for (int x = 0; x < formation.NCells.x; x++)
        {
            for (int y = 0; y < formation.NCells.y; y++)
            {
                Vector2Int coordinate = new Vector2Int(x, y);
                Vector3 worldPosition = formation.CoordinateToWorldPosition(coordinate);
                bool onFormation = formation.WorldPositionToCoordinate(worldPosition, out var backToCoordinate);

                Handles.Label(worldPosition, $"{worldPosition.x.ToString("0")}, {worldPosition.y.ToString("0")}\n{backToCoordinate.x}, {backToCoordinate.y}");
            }
        }
    }
}
