
// TODO: this stuff only applies selectively
// e.g. only applies when targeting foreign formations
// e.g.
//public enum TargetableCells
//{
//    /// <summary>
//    /// Can taget any cell on target formation.
//    /// </summary>
//    Any,
//    /// <summary>
//    /// Can target cells in the same row / column
//    /// on the target formation.
//    /// </summary>
//    Linear,
//    /// <summary>
//    /// Can target exposed cells in the same row / 
//    /// column on the target formation.
//    /// </summary>
//    LinearExposed,
//    /// <summary>
//    /// Can target cells in a given rank on the
//    /// target formation.
//    /// </summary>
//    Rank,
//    /// <summary>
//    /// Can target exposed cells in any rank on
//    /// the target formation.
//    /// </summary>
//    RankExposed
//}

using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableCells
{
    protected BattleAction action;
    public TargetableCells(BattleAction action)
    {
        this.action = action;
    }

    public abstract IEnumerable<(Formation, Vector2Int)> GetTargetableCells();
}