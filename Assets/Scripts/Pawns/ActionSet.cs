﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class ActionSet : MonoBehaviour
{
    [Tooltip("Occurs when one of the pawn's actions are used.")]
    public UnityEvent<ActionSet, BattleAction> OnActionUsed;

    // Properties

    /// <summary>
    /// Gets all the actions.
    /// </summary>
    public List<BattleAction> Actions { get; private set; } = new List<BattleAction>();
    /// <summary>
    /// Gets an action by index.
    /// </summary>
    public BattleAction this[int index] { get { return Actions[index]; } }
    /// <summary>
    /// Gets an action by name.
    /// </summary>
    public BattleAction this[string name] { get { return Actions.FirstOrDefault(a => a.name == name); } }
    /// <summary>
    /// Gets the number of actions.
    /// </summary>
    public int Count { get { return Actions.Count; } }

    // Components
    private Pawn pawn;

    // Fields
    private bool initialized;

    private void Awake()
    {
        pawn = GetComponent<Pawn>();
        pawn.OnTurnStarted.AddListener(HandleOnPawnTurnStarted);
        pawn.OnTurnEnded.AddListener(HandleOnPawnTurnEnded);
    }

    /// <summary>
    /// Adds the given action to the list of actions.
    /// </summary>
    public BattleAction Add(BattleAction action)
    {
        Actions.Add(action);
        action.OnActionUsed += HandleOnActionUsed;
        return action;
    }

    /// <summary>
    /// Checks if there are any actions that are able to be used.
    /// </summary>
    public bool AnyAvailableAction()
    {
        return Actions.Any(a => a.CanDo());
    }

    /// <summary>
    /// Gets the total number of actions used this turn.
    /// </summary>
    public int GetTotalActionUseCount()
    {
        return Actions.Sum(a => a.UseCount);
    }

    /// <summary>
    /// Gets the number of times the given action has been used this turn.
    /// </summary>
    public int GetActionUseCount(string name)
    {
        BattleAction action = Actions.FirstOrDefault(a => a.name == name);
        if (action != null)
            return action.UseCount;
        return -1;
    }

    private void HandleOnActionUsed(BattleAction action)
    {
        OnActionUsed?.Invoke(this, action);
    }

    private void HandleOnPawnTurnStarted(Pawn pawn)
    {
        if (!initialized)
        {
            // Each action is always associated with the given pawn.
            foreach (BattleAction action in Actions)
                action.SetActor(pawn);

            initialized = true;
        }
    }

    private void HandleOnPawnTurnEnded(Pawn pawn)
    {
        foreach (BattleAction action in Actions)
            action.ClearUses();
    }
}
