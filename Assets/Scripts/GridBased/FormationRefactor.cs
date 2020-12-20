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

    private bool Contains(Cell cell)
    {
        return Contains(cell.Coordinate);
    }

    private bool Contains(Vector2Int coordinate)
    {
        return Contains(coordinate.x, coordinate.y);
    }

    private bool Contains(int x, int y)
    {
        return x >= origin.x &&
                x < (origin.x + nCells.x) &&
                y >= origin.y &&
                y <= (origin.y + nCells.y);
    }

    private IEnumerable<Cell> GetCells()
    {
        foreach (Vector2Int coordinate in GetCoordinates())
            yield return Grid.GetCell(coordinate);
    }

    private IEnumerable<Cell> GetRankCells(Vector2Int reference, int rank)
    {
        foreach (Vector2Int coordinate in GetRankCoordinates(reference, rank))
            yield return Grid.GetCell(coordinate);
    }

    private IEnumerable<Vector2Int> GetRankCoordinates(Vector2Int reference, int rank)
    {
        // Coordinate at front of formation relative to the reference.
        Vector2Int frontOrigin = GetFrontOrigin(reference);

        // Increment to step through all coordinates in the given rank.
        Vector2Int step = GetRankStep(reference);

        // Coordinate at the start of the given rank
        Vector2Int rankOrigin = frontOrigin + step.Perpendicular() * rank;

        // Direction and length of the rank.
        Vector2Int rankVector = step * nCells;

        // Number of files in rank.
        int steps = (int)rankVector.magnitude;

        // Always return the origin (in case formation is a single cell)
        yield return rankOrigin;
        for (int i = 1; i < steps; i++)
            yield return rankOrigin + (i * step);
    }

    //private IEnumerable<Vector2Int> GetFrontCoordinates(Vector2Int reference)
    //{
    //    // Coordinate at front of formation relative to the reference.
    //    Vector2Int frontOrigin = GetFrontOrigin(reference);

    //    // Increment to step through all coordinates in front rank.
    //    Vector2Int step = GetFrontStep(reference);

    //    // Direction and length of front of formation.
    //    Vector2Int frontVector = step * nCells;

    //    // Number of files in front rank.
    //    int steps = (int)frontVector.magnitude;

    //    // Always return the origin (in case formation is a single cell)
    //    yield return frontOrigin;
    //    for (int i = 1; i < steps; i++)
    //        yield return frontOrigin + (i * step);
    //}

    private bool WithinXRange(Vector2Int coordinate)
    {
        return coordinate.x >= origin.x &&
            coordinate.x <= origin.x + nCells.x;
    }

    private bool WithinYRange(Vector2Int coordinate)
    {
        return coordinate.y >= origin.y &&
            coordinate.y <= origin.y + nCells.y;
    }

    private Vector2Int GetRankStep(Vector2Int reference)
    {
        Vector2Int direction = reference - origin;
        Vector2Int step = direction.Perpendicular().Reduce();
        
        // Reverse the direction of step so we move away from the edge.
        if (reference.sqrMagnitude < origin.sqrMagnitude)
            step = -step;
        
        return step;
    }

    private Vector2Int GetFrontOrigin(Vector2Int reference)
    {
        bool withinX = WithinXRange(reference);
        bool withinY = WithinYRange(reference);

        Vector2Int frontOrigin = origin;
        if (withinY)
        {
            if (reference.x > origin.x)
                frontOrigin.x = origin.x + nCells.x;
        }

        else if (withinX)
        {
            if (reference.y > origin.y)
                frontOrigin.y = origin.y + nCells.y;
        }

        return frontOrigin;
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
