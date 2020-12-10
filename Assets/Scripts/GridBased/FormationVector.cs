using UnityEngine;
using System.Collections;

public struct FormationVector
{
    public static FormationVector Origin = new FormationVector(0, 0);
    public static FormationVector Forward = new FormationVector(-1, 0);
    public static FormationVector Back = -Forward;

    public int Rank;
    public int File;

    public FormationVector(int rank, int file)
    {
        Rank = rank;
        File = file;
    }

    public static FormationVector operator +(FormationVector a, FormationVector b)
    {
        return new FormationVector(a.Rank + b.Rank, a.File + b.File);
    }

    public static FormationVector operator -(FormationVector a, FormationVector b)
    {
        return new FormationVector(a.Rank - b.Rank, a.File - b.File);
    }

    public static FormationVector operator -(FormationVector a)
    {
        return new FormationVector(-a.Rank, -a.File);
    }

    public static FormationVector operator *(FormationVector a, FormationVector b)
    {
        return new FormationVector(a.Rank * b.Rank, a.File * b.File);
    }

    public static FormationVector operator *(FormationVector a, int factor)
    {
        return new FormationVector(a.Rank * factor, a.File * factor);
    }

    public static FormationVector operator /(FormationVector a, FormationVector b)
    {
        return new FormationVector(a.Rank / b.Rank, a.File / b.File);
    }

    public static FormationVector operator /(FormationVector a, int factor)
    {
        return new FormationVector(a.Rank / factor, a.File / factor);
    }
}
