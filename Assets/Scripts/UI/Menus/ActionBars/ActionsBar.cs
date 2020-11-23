using System;
using UnityEngine;
using UnityEngine.UI;


public class ActionsBar : Menu
{
    private int ActionCount { get { return ActionManager.Instance.SelectedActor.Actions.Count; } }

    [SerializeField]
    private Button[] actionButtons;

    protected override void Awake()
    {
        base.Awake();

        ActionManager.Instance.OnActorSelected += HandleOnActorSelected;
        ActionManager.Instance.OnActorDeselected += HandleOnActorDeselected;
        ActionManager.Instance.OnActionStarted += HandleOnActionStarted;
    }

    protected override void PreShow()
    {
        base.PreShow();

        // Populate bar actions with actor's attacks
        for (int i = 0; i < actionButtons.Length; i++)
        {
            // Enable / Disable button
            bool enabled = i < ActionCount;
            actionButtons[i].gameObject.SetActive(enabled);

            if (enabled)
            {
                // Set text to action name
                Text text = actionButtons[i].GetComponentInChildren<Text>();
                text.text = ActionManager.Instance.SelectedActor.Actions[i];
            }
        }
    }

    public void OnActionTapped(int index)
    {
        string action = ActionManager.Instance.SelectedActor.Actions[index];
        ActionManager.Instance.SelectAction(action + "Action");
    }

    public void OnEndTurnTapped()
    {
        ActionManager.Instance.EndSelectedActorTurn();
    }

    public void OnCancelTapped()
    {
        ActionManager.Instance.ClearSelectedAction();
    }

    private void HandleOnActorSelected(Actor obj)
    {
        MenuStack.Instance.Show(this);
    }

    private void HandleOnActorDeselected(Actor obj)
    {
        MenuStack.Instance.Hide();
    }

    private void HandleOnActionStarted(Actor arg1, BattleAction arg2)
    {
        MenuStack.Instance.HideAll();
    }
}
