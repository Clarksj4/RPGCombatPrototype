using UnityEngine;
using System.Collections;


public class ActionsBar : Menu
{
    public void OnAttackTapped()
    {
        MenuStack.Instance.Show<AttacksBar>();
    }

    public void OnMoveTapped()
    {
        ActionManager.Instance.SelectAction<MoveAction>();
        MenuStack.Instance.Show<CancelBar>();
    }

    public void OnEndTurnTapped()
    {
        ActionManager.Instance.EndSelectedActorTurn();
        MenuStack.Instance.CloseAll();
    }

    public void OnCancelTapped()
    {
        ActionManager.Instance.ClearSelectedAction();
        MenuStack.Instance.CloseCurrent();
    }
}
