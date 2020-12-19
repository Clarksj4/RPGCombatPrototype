using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class FormationRefactor : MonoBehaviour
{
    public MonoGrid Grid { get { return GetComponentInParent<MonoGrid>(); } }
    public Vector3 Size { get { return nCells * Grid.LossyCellSize; } }

    // TODO: collection of pawns in this formation
    private Team team;
    private List<Pawn> pawns;

    [SerializeField]
    private Vector2Int nCells;
    [SerializeField]
    private Vector2Int origin;

    private void OnDrawGizmos()
    {
        // TODO: draw lines to corners so rotation is taken into account
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Size);
        Gizmos.color = Color.white;
    }

    private void OnValidate()
    {
        Vector3 originWorldPosition = Grid.CoordinateToWorldPosition(origin);
        transform.position = originWorldPosition + (Size / 2) - (Vector3)(Grid.LossyCellSize / 2);
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
