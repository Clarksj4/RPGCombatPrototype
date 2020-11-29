﻿using UnityEngine;
using UnityEditor;

//[RequireComponent(typeof(Grid))]
public class GridRenderer : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Transform cellParent;

    public void SetCellColour(Vector2Int coordinate, Color colour)
    {
        Transform cell = GetCellAtCoordinate(coordinate);
        SpriteRenderer renderer = cell.GetComponent<SpriteRenderer>();
        renderer.color = colour;
        renderer.sortingOrder++;
    }

    public void SetAllCellColours(Color colour)
    {
        foreach (Transform cell in cellParent)
        {
            SpriteRenderer renderer = cell.GetComponent<SpriteRenderer>();
            renderer.color = colour;
            renderer.sortingOrder = -10;
        }
            
    }

    public void SetCellColour(int x, int y, Color colour)
    {
        //SetCellColour(coordinate.x, coordinate.y, colour);
    }

    private Transform GetCellAtCoordinate(Vector2Int coordinate)
    {
        int index = coordinate.x + (coordinate.y * grid.NCells.x);
        return cellParent.GetChild(index);
    }

    [ContextMenu("ArrangeCells")]
    private void ArrangeCells()
    {
        for (int i = 0; i < cellParent.childCount; i++)
        {
            int y = i / grid.NCells.x;
            int x = i % grid.NCells.x;
            Vector2Int coordinate = new Vector2Int(x, y);

            Transform child = cellParent.GetChild(i);
            child.name = new Vector2Int(x, y).ToString();

            child.position = grid.CoordinateToWorldPosition(coordinate);
        }
    }
}