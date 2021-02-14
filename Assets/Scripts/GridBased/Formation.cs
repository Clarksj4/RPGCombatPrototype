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
    private Vector2Int nCells = default;
    [SerializeField]
    private Vector2Int origin = default;
    [SerializeField]
    private Vector2Int facing = default;

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

    public Vector2Int GetDirection(FormationMovement movement)
    {
        switch (movement)
        {
            case FormationMovement.AdvanceRank:
                return Facing;
            case FormationMovement.RetreatRank:
                return -Facing;
            case FormationMovement.IncrementFile:
                return Facing.Perpendicular();
            case FormationMovement.DecrementFile:
                return -Facing.Perpendicular();
            default:
                return Vector2Int.zero;
        }
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

    public int GetFile(Vector2Int coordinate)
    {
        // Get the distance from the front rank
        Vector2Int frontOrigin = GetFrontOrigin();
        Vector2Int delta = frontOrigin - coordinate;

        // Delete information in the axis we don't care about
        delta *= facing.Perpendicular().Abs();

        // Remaining axis contains file
        return delta.Abs().MaxAxisMagnitude();
    }

    public IEnumerable<Cell> GetFileCells(int file)
    {
        foreach (Vector2Int coordinate in GetFileCoordinates(file))
            yield return Grid.GetCell(coordinate);
    }

    public IEnumerable<Vector2Int> GetFileCoordinates(int file)
    {
        // Coordinate at front of formation relative to the reference.
        Vector2Int frontOrigin = GetFrontOrigin();

        Vector2Int stepAwayFromOrigin = facing.Perpendicular().Abs();
        Vector2Int fileOrigin = frontOrigin + (stepAwayFromOrigin * file);

        Vector2Int stepAlongFile = -facing;

        int steps = NFiles;

        // Always return the origin (in case formation is a single cell)
        yield return fileOrigin;
        for (int i = 1; i < steps; i++)
        {
            Vector2Int coordinate = fileOrigin + (i * stepAlongFile);
            if (Grid.Contains(coordinate) && Contains(coordinate))
                yield return coordinate;
        }
    }

    public int GetRank(Vector2Int coordinate)
    {
        // Get the distance from the front rank
        Vector2Int frontOrigin = GetFrontOrigin();
        Vector2Int delta = frontOrigin - coordinate;

        // Delete information in the axis we don't care about
        delta *= facing.Abs();

        // Remaining axis contains rank
        return delta.Abs().MaxAxisMagnitude();
    }

    public IEnumerable<Cell> GetRankCells(int rank)
    {
        foreach (Vector2Int coordinate in GetRankCoordinates(rank))
            yield return Grid.GetCell(coordinate);
    }

    public IEnumerable<Vector2Int> GetRankCoordinates(int rank)
    {
        // Coordinate at front of formation relative to the reference.
        Vector2Int frontOrigin = GetFrontOrigin();

        Vector2Int stepAwayFromFront = -facing;
        Vector2Int rankOrigin = frontOrigin + (stepAwayFromFront * rank);

        Vector2Int stepAlongRank = stepAwayFromFront.Perpendicular().Abs();

        int steps = NFiles;

        // Always return the origin (in case formation is a single cell)
        yield return rankOrigin;
        for (int i = 1; i < steps; i++)
        {
            Vector2Int coordinate = rankOrigin + (i * stepAlongRank);
            if (Grid.Contains(coordinate) && Contains(coordinate))
                yield return coordinate;
        }
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
