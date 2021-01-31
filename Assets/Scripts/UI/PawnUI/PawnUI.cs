using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PawnUI : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar = null;
    private Pawn pawn = null;

    private void Awake()
    {
        pawn = GetComponentInParent<Pawn>();
        pawn.OnHealthChanged += HandleOnPawnHealthChanged;

        ActionManager.Instance.OnTargetSelected += HandleOnActionTargetSelected;
    }

    private void HandleOnPawnHealthChanged(int change)
    {
        float fill = (float)pawn.Health / pawn.MaxHealth;
        healthBar.SetHealth(fill);
    }

    private void HandleOnActionTargetSelected(BattleAction action)
    {
        if (action.AffectedCells.Contains(pawn.Cell))
        {
            // TODO: check actions nodes for damage node
            DoDamageNode damageNode = action.TargetedActions.FirstOfTypeOrDefault<ActionNode, DoDamageNode>();
        }
    }
}
