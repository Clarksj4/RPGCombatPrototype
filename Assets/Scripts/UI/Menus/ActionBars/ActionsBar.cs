using System;
using UnityEngine;
using UnityEngine.UI;


public class ActionsBar : Menu
{
    private int ActionCount { get { return ActionManager.Instance.SelectedActor.Actions.Count; } }

    [SerializeField]
    private ActionButton[] actionButtons = null;

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

    private void RefreshButtons()
    {
        // Populate bar actions with actor's attacks
        for (int i = 0; i < actionButtons.Length; i++)
        {
            ActionButton button = actionButtons[i];

            // Enable / Disable button
            bool enabled = i < ActionCount;
            button.gameObject.SetActive(enabled);

            if (enabled)
            {
                string actionName = ActionManager.Instance.SelectedActor.Actions[i];
                BattleAction action = ActionManager.Instance.CreateAction(actionName + "Action");
                button.SetAction(action);
            }
        }
    }

    public void OnActionTapped(int index)
    {
        ActionManager.Instance.SelectAction(index);
    }

    public void OnEndTurnTapped()
    {
        ActionManager.Instance.EndSelectedActorTurn();
    }

    public void OnCancelTapped()
    {
        ActionManager.Instance.ClearSelectedAction();
    }

    private void HandleOnActorSelected(Pawn actor)
    {
        MenuStack.Instance.Show(this);
    }

    private void HandleOnActorDeselected(Pawn actor)
    {
        MenuStack.Instance.Hide();
    }

    private void HandleOnActionStarted(Pawn actor, BattleAction action)
    {
        MenuStack.Instance.Hide();
    }

    private void HandleOnActionComplete(Pawn actor, BattleAction action)
    {
        RefreshButtons();
    }
}
