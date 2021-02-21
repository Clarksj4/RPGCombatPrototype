using System.Collections.Generic;
using UnityEngine;

public class ActionHighlighter : MonoSingleton<ActionHighlighter>
{
    /// <summary>
    /// Gets the currently selected action.
    /// </summary>
    private BattleAction SelectedAction { get { return ActionManager.Instance.SelectedAction; } }

    [Tooltip("The grid renderer to colour in.")]
    [SerializeField] private GridRenderer gridRenderer = null;

    protected override void Awake()
    {
        base.Awake();

        ActionManager.Instance.OnActionSelected += HandleOnActionSelected;
        ActionManager.Instance.OnActionDeselected += HandleOnActionDeselected;
        ActionManager.Instance.OnTargetSelected += HandleOnTargetSelected;
        ActionManager.Instance.OnTargetDeselected += HandleOnTargetDeselected;
        ActionManager.Instance.OnActionStarted += HandleOnActionStarted;
    }

    private void Start()
    {
        // Start with nothing highlighted
        UnhighlightAll();
    }

    /// <summary>
    /// Highlight all the cells that could be targeted with
    /// the current action.
    /// </summary>
    private void HighlightPossibleTargets()
    {
        SetCellColour(
            cells: GetPossibleTargets(),
            colour: Color.white
        );
    }

    /// <summary>
    /// Highlight all the cells that will be affected by the
    /// current action when used at the targeted cell.
    /// </summary>
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
        // TODO: get coloud based on action nodes

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
            gridRenderer.SetCellColour(cell, colour);
    }

    private IEnumerable<Cell> GetAffectedCoordinates()
    {
        return SelectedAction.AffectedCells;
    }

    private IEnumerable<Cell> GetPossibleTargets()
    {
        return SelectedAction.TargetableCells;
    }

    private void HandleOnTargetSelected(BattleAction action)
    {
        // Highlight all the cells that will be affected.
        UnhighlightAll();
        HighlightAffectedCells();
    }

    private void HandleOnTargetDeselected(BattleAction action)
    {
        // Highlight all the possible targets.
        UnhighlightAll();
        HighlightPossibleTargets();
    }

    private void HandleOnActionSelected(BattleAction action)
    {
        // Highlight all the possible targets.
        UnhighlightAll();
        HighlightPossibleTargets();
    }

    private void HandleOnActionDeselected(BattleAction action)
    {
        // Nothing to highlight.
        UnhighlightAll();
    }

    private void HandleOnActionStarted(Pawn pawn, BattleAction action)
    {
        // Stop highlighting things so we don't obscure the view
        // of the action being carried out.
        UnhighlightAll();
    }
}
