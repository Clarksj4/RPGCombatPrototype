using UnityEngine;
using UnityEngine.UI;


public class ActionsBar : Menu
{
    private int ActionCount { get { return ActionManager.Instance.SelectedActor.Actions.Count; } }

    [SerializeField]
    private Button[] actionButtons;

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
        MenuStack.Instance.Show<CancelBar>();
    }

    public void OnEndTurnTapped()
    {
        ActionManager.Instance.EndSelectedActorTurn();
        MenuStack.Instance.HideAll();
    }

    public void OnCancelTapped()
    {
        ActionManager.Instance.ClearSelectedAction();
        MenuStack.Instance.HideAll();
    }
}
