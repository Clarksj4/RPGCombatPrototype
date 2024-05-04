using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField]
    private string actionOverride = null;
    [Space(10)]
    [SerializeField]
    private UnityEvent<bool> OnTapped = null;
    [SerializeField]
    private UnityEvent OnRefresh = null;

    public BattleAction Action => action;
    private BattleAction action = null;

    private void Awake()
    {
        ActionManager.Instance.OnActionComplete += HandleOnActionComplete;

        if (!string.IsNullOrEmpty(actionOverride))
            ActionManager.Instance.OnActorSelected += HandleOnActorSelected;
    }

    public void OnTap()
    {
        if (action != null)
        {
            bool canDo = action.CanDo();
            if (canDo)
                ActionManager.Instance.SelectAction(action);

            OnTapped?.Invoke(canDo);
        }
    }

    public void SetAction(BattleAction action)
    {
        this.action = action;
        Refresh();
    }

    private void Refresh()
    {
        OnRefresh?.Invoke();
    }

    private void HandleOnActionComplete(Pawn pawn, BattleAction action)
    {
        if (gameObject.activeSelf)
            Refresh();
    }

    private void HandleOnActorSelected(Pawn pawn)
    {
        if (!string.IsNullOrEmpty(actionOverride))
            SetAction(pawn.Actions[actionOverride]);
    }
}
