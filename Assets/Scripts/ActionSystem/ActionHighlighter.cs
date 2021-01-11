using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ActionHighlighter : MonoBehaviour
    {
        // Convenience properties
        private BattleAction SelectedAction { get { return ActionManager.Instance.SelectedAction; } }

        [SerializeField]
        private GridRenderer gridRenderer;

        private void Awake()
        {
            ActionManager.Instance.OnActionSelected += HandleOnActionSelected;
            ActionManager.Instance.OnActionDeselected += HandleOnActionDeselected;
            ActionManager.Instance.OnTargetSelected += HandleOnTargetSelected;
            ActionManager.Instance.OnTargetDeselected += HandleOnTargetDeselected;
            ActionManager.Instance.OnActionStarted += HandleOnActionStarted;
        }

        private void Start()
        {
            UnhighlightAll();
        }

        private void HighlightPossibleTargets()
        {
            SetCellColour(
                cells: GetPossibleTargets(),
                colour: Color.white
            );
        }

        private void HighlightAffectedCells()
        {
            SetCellColour(
                cells: GetAffectedCoordinates(),
                colour: GetActionColour()
            );
        }

        private void UnhighlightAll()
        {
            // Unhighlight everything
            gridRenderer.SetAllCellColours(Color.grey);
        }

        private Color GetActionColour()
        {
            Color colour = Color.cyan;

            if (SelectedAction.Tags.HasFlag(ActionTag.Damage))
                colour = Color.red;

            else if (SelectedAction.Tags.HasFlag(ActionTag.Heal))
                colour = Color.green;

            return colour;
        }

        private void SetCellColour(IEnumerable<Cell> cells, Color colour)
        {
            // Highlight possible targets 
            foreach (Cell cell in cells)
                gridRenderer.SetCellColour(cell.Coordinate, colour);
        }

        private IEnumerable<Cell> GetAffectedCoordinates()
        {
            return SelectedAction.GetAffectedCoordinates();
        }

        private IEnumerable<Cell> GetPossibleTargets()
        {
            return SelectedAction.TargetableCells;
        }

        private void HandleOnTargetSelected(BattleAction action)
        {
            UnhighlightAll();
            HighlightPossibleTargets();
            HighlightAffectedCells();
        }

        private void HandleOnTargetDeselected(BattleAction action)
        {
            UnhighlightAll();
            HighlightPossibleTargets();
        }

        private void HandleOnActionSelected(BattleAction action)
        {
            HighlightPossibleTargets();
        }

        private void HandleOnActionDeselected(BattleAction action)
        {
            UnhighlightAll();
        }

        private void HandleOnActionStarted(Actor actor, BattleAction action)
        {
            UnhighlightAll();
        }
    }
}