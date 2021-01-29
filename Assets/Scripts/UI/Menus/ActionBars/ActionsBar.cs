using UnityEngine;
using UnityEngine.UI;


public class ActionsBar : Menu
{
    private int ActionCount { get { return ActionManager.Instance.SelectedActor.Actions.Count; } }

    [SerializeField]
    private Button[] actionButtons = null;

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
            Button button = actionButtons[i];

            // Enable / Disable button
            bool enabled = i < ActionCount;
            button.gameObject.SetActive(enabled);

            if (enabled)
            {
                string actionName = ActionManager.Instance.SelectedActor.Actions[i];

                // Set text to action name
                Text text = actionButtons[i].GetComponentInChildren<Text>();
                text.text = actionName;
                button.interactable = ActionManager.Instance.CanDo(actionName + "Action");
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
        MenuStack.Instance.HideAll();
    }
}
