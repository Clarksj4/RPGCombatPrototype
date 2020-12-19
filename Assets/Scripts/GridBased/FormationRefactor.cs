using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class FormationRefactor : MonoBehaviour
{
    public MonoGrid Grid { get { return GetComponentInParent<MonoGrid>(); } }
    public Vector3 Size { get { return nCells * Grid.CellSize; } }
    public Vector3 Extents { get { return Size / 2; } }

    // TODO: collection of pawns in this formation
    private Team team;
    private List<Pawn> pawns;

    [SerializeField]
    private Vector2Int nCells;
    [SerializeField]
    private Vector2Int origin;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 corner1 = transform.TransformPoint(new Vector3(-Extents.x, -Extents.y));
        Vector3 corner2 = transform.TransformPoint(new Vector3(-Extents.x, Extents.y));
        Vector3 corner3 = transform.TransformPoint(new Vector3(Extents.x, Extents.y));
        Vector3 corner4 = transform.TransformPoint(new Vector3(Extents.x, -Extents.y));

        Gizmos.DrawLine(corner1, corner2);
        Gizmos.DrawLine(corner2, corner3);
        Gizmos.DrawLine(corner3, corner4);
        Gizmos.DrawLine(corner4, corner1);
    }

    private void OnValidate()
    {
        Vector3 localPosition = -(Vector3)Grid.Extents + (Vector3)(origin * Grid.CellSize) + (Vector3)((nCells * Grid.CellSize) / 2f);
        transform.localPosition = localPosition;
    }

    private IEnumerable<Vector2Int> GetCoordinates()
    {
        for (int x = 0; x < nCells.x; x++)
        {
            for (int y = 0; y < nCells.y; y++)
                yield return origin + new Vector2Int(x, y);
        }
    }
}
