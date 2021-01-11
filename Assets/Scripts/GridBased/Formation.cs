using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Formation : MonoBehaviour
{
    /// <summary>
    /// Gets the grid this formation is a part of.
    /// </summary>
    public MonoGrid Grid { get { return GetComponentInParent<MonoGrid>(); } }
    /// <summary>
    /// Gets the width and height of this formation
    /// </summary>
    public Vector3 Size { get { return nCells * Grid.CellSize; } }
    /// <summary>
    /// Gets the extents of this formation, equal to half the width and height.
    /// </summary>
    public Vector3 Extents { get { return Size / 2; } }
    /// <summary>
    /// Gets the direction that this formation is facing.
    /// </summary>
    public Vector2Int Facing { get { return facing; } }
    /// <summary>
    /// Gets all the pawns currently in this formation.
    /// </summary>
    public IEnumerable<Pawn> Pawns 
    { 
        get 
        { 
            return GetCells().SelectMany(c => c.Contents)
                             .Where(c => c is Pawn)
                             .Select(c => c as Pawn); 
        } 
    }

    public int NFiles 
    { 
        get
        {
            Vector2Int asVector = nCells * facing.Perpendicular();
            int axisMagnitude = asVector.MaxAxisMagnitude();
            int nFiles = Mathf.Abs(axisMagnitude);
            return nFiles;
        } 
    }

    public int NRanks
    {
        get
        {
            Vector2Int asVector = nCells * facing;
            int axisMagnitude = asVector.MaxAxisMagnitude();
            int nRanks = Mathf.Abs(axisMagnitude);
            return nRanks;
        }
    }

    [SerializeField]
    private Vector2Int nCells;
    [SerializeField]
    private Vector2Int origin;
    [SerializeField]
    private Vector2Int facing;

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

    public bool Contains(Cell cell)
    {
        return Contains(cell.Coordinate);
    }

    public bool Contains(Vector2Int coordinate)
    {
        return Contains(coordinate.x, coordinate.y);
    }

    public bool Contains(int x, int y)
    {
        return x >= origin.x &&
                x <= (origin.x + nCells.x) &&
                y >= origin.y &&
                y <= (origin.y + nCells.y);
    }

    public IEnumerable<Cell> GetCells()
    {
        foreach (Vector2Int coordinate in GetCoordinates())
            yield return Grid.GetCell(coordinate);
    }

    public IEnumerable<Vector2Int> GetCoordinates()
    {
        for (int x = 0; x < nCells.x; x++)
        {
            for (int y = 0; y < nCells.y; y++)
                yield return origin + new Vector2Int(x, y);
        }
    }

    public IEnumerable<Cell> GetRankCells(int rank)
    {
        foreach (Vector2Int coordinate in GetRankCoordinates(rank))
        {
            Cell cell = Grid.GetCell(coordinate);
            print(coordinate);
            print(cell.name);
            yield return cell;
        }
    }

    public IEnumerable<Vector2Int> GetRankCoordinates(int rank)
    {
        // Coordinate at front of formation relative to the reference.
        Vector2Int frontOrigin = GetFrontOrigin();

        Vector2Int stepAwayFromFront = -facing;
        Vector2Int rankOrigin = frontOrigin + (stepAwayFromFront * rank);

        Vector2Int stepAlongRank = stepAwayFromFront.Perpendicular().Abs();

        int steps = NFiles;

        //print($"origin: {origin}, nCells: {nCells}");

        // Always return the origin (in case formation is a single cell)
        yield return rankOrigin;
        for (int i = 1; i < steps; i++)
        {
            Vector2Int coordinate = rankOrigin + (i * stepAlongRank);
            bool gridContains = Grid.Contains(coordinate);
            bool formationContains = Contains(coordinate);
            //print($"{coordinate}, gridContains: {gridContains}, formationContains: {formationContains}");
            if (Grid.Contains(coordinate) && Contains(coordinate))
                yield return coordinate;
        }
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

    private Vector2Int Clamp(Vector2Int coordinate)
    {
        int x = Mathf.Clamp(coordinate.x, origin.x, origin.x + nCells.x - 1);
        int y = Mathf.Clamp(coordinate.y, origin.y, origin.y + nCells.y - 1);
        return new Vector2Int(x, y);
    }

    private Vector2Int GetFrontOrigin()
    {
        Vector2Int step = facing * nCells;
        Vector2Int towardsFront = origin + step;
        return Clamp(towardsFront);
    }
}
