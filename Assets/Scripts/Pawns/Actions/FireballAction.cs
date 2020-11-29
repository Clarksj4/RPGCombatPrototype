using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireballAction : BattleAction
{
    /// <summary>
    /// The minimum chance for an attack to hit.
    /// </summary>
    private const float MINIMUM_HIT_CHANCE = 0.1f;
    /// <summary>
    /// The area from the targeted cell that will be affected.
    /// </summary>
    private const int AREA = 1;

    public override ActionTag Tags { get { return ActionTag.Damage; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Enemy | TargetableCellContent.Empty; } }
    public override TargetableFormations TargetableFormations { get { return TargetableFormations.Other; } }

    public override bool IsTargetValid(Formation formation, Vector2Int position)
    {
        bool valid = base.IsTargetValid(formation, position);
        if (!valid) return false;

        Vector2Int closestCoordinate = formation.GetClosestCoordinate(Actor.WorldPosition);
        if (position == closestCoordinate)
            return true;

        Vector2 closestCellWorldPosition = formation.CoordinateToWorldPosition(closestCoordinate);
        Vector2 direction = closestCellWorldPosition - Actor.WorldPosition;
        direction.Normalize();

        Vector2Int directionCoordinate = new Vector2Int(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y));
        directionCoordinate.Scale(formation.NCells);

        IEnumerable<Vector2Int> line = formation.GetCoordinatesInLine(closestCoordinate, directionCoordinate);
        return line.Contains(position);
    }

    public override IEnumerable<(Formation, Vector2Int)> GetAffectedCoordinates()
    {
        foreach (Vector2Int coordinate in TargetFormation.GetCoordinatesInRange(TargetPosition, AREA))
            yield return (TargetFormation, coordinate);
    }

    public override IEnumerator Do()
    {
        foreach ((Formation formation, Vector2Int coordinate) in GetAffectedCoordinates())
        {
            Pawn defender = formation.GetPawnAtCoordinate(coordinate);
            if (defender != null && IsHit(defender))
                ApplyDamage(defender);
        }

        return null;
    }

    // Does the attack hit?
    private bool IsHit(Pawn defender)
    {
        // Always have a minimum chance to hit
        float hitChance = Mathf.Max(MINIMUM_HIT_CHANCE, Actor.Accuracy - defender.Evasion);
        bool hits = Random.Range(0f, 1f) <= hitChance;
        Debug.Log("Attack hits!");
        return hits;
    }

    // How much damage does the attack do?
    private void ApplyDamage(Pawn defender)
    {
        // Damage can't be below 0
        int damage = (int)Mathf.Max(0, Actor.Attack - defender.Defense);
        Debug.Log($"Defender takes {damage} damage.");
        defender.Health -= damage;
    }
}
