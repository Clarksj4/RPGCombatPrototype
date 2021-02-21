﻿using UnityEngine;

public class ActionsBar : Menu
{
    /// <summary>
    /// Gets the number of actions the currently selected actor has.
    /// </summary>
    private int ActionCount { get { return ActionManager.Instance.SelectedActor.BattleActions.Count; } }

    [Tooltip("All of the buttons for displaying actions.")]
    [SerializeField] private ActionButton[] actionButtons = null;

    protected override void Awake()
    {
        base.Awake();

        ActionManager.Instance.OnActorSelected += HandleOnActorSelected;
        ActionManager.Instance.OnActorDeselected += HandleOnActorDeselected;
        ActionManager.Instance.OnActionStarted += HandleOnActionStarted;
        ActionManager.Instance.OnActionComplete += HandleOnActionComplete;
    }

    protected override void PreShow()
    {
        base.PreShow();

        RefreshButtons();
    }
    
    /// <summary>
    /// Update the actions shown on the buttons and their visibility. 
    /// </summary>
    private void RefreshButtons()
    {
        // Populate buttons with actor's actions
        for (int i = 0; i < actionButtons.Length; i++)
        {
            ActionButton button = actionButtons[i];

            // Enable / Disable button
            bool enabled = i < ActionCount;
            button.gameObject.SetActive(enabled);

            if (enabled)
            {
                // Set the button's associated action.
                BattleAction action = ActionManager.Instance.SelectedActor.BattleActions[i];
                button.SetAction(action);
            }
        }
    }

    public void OnActionTapped(int index)
    {
        // Set action as the one the actor is going to use.
        ActionManager.Instance.SelectActionByIndex(index);
    }

    public void OnEndTurnTapped()
    {
        // End actor's turn
        ActionManager.Instance.EndSelectedActorTurn();
    }

    private void HandleOnActorSelected(Pawn actor)
    {
        // Show action bar for the given actor.
        MenuStack.Instance.Show(this);
    }

    private void HandleOnActorDeselected(Pawn actor)
    {
        // Hide action bar - there's no actor to display actions for.
        MenuStack.Instance.Hide();
    }

    private void HandleOnActionStarted(Pawn actor, BattleAction action)
    {
        // Hide the action bar when the action starts so it's not in the way
        MenuStack.Instance.Hide();
    }

    private void HandleOnActionComplete(Pawn actor, BattleAction action)
    {
        // Refresh the buttons when the action is finished so that
        // their mana costs etc are updated.
        RefreshButtons();
    }
}
