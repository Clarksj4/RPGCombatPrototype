using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, ITurnBased
{
    public float Priority { get; set; }
    public string Name { get; set; }
    public Vector2Int MapPosition { get; private set; }
    public BattleMap BattleMap { get { return GetComponentInParent<BattleMap>(); } }

    private void OnMouseDown()
    {
        print("Actor tapped");
    }

    public void Move(Vector2Int destination)
    {
        MapPosition = destination;
        transform.position = BattleMap.GetWorldPositionFromCoordinate(destination);
    }

    public void Attack(int attackIndex, Vector2Int target)
    {

    }

    public void EndTurn()
    {

    }
}
