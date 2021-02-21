using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    [Tooltip("The grid to control the colouring of.")]
    [SerializeField] private MonoGrid grid = null;

    /// <summary>
    /// Sets the colour of the given cell.
    /// </summary>
    public void SetCellColour(Cell cell, Color colour)
    {
        SpriteRenderer renderer = cell.GetComponent<SpriteRenderer>();
        renderer.color = colour;
        renderer.sortingOrder++;
    }

    /// <summary>
    /// Sets the colour of the cell at the given coordinate.
    /// </summary>
    public void SetCellColour(Vector2Int coordinate, Color colour)
    {
        Cell cell = grid.GetCell(coordinate);
        SetCellColour(cell, colour);
    }

    /// <summary>
    /// Sets the colour of all the cells in the grid to the given colour.
    /// </summary>
    public void SetAllCellColours(Color colour)
    {
        foreach (Cell cell in grid.GetCells())
        {
            SpriteRenderer renderer = cell.GetComponent<SpriteRenderer>();
            renderer.color = colour;
            renderer.sortingOrder = -10;
        }
    }
}
