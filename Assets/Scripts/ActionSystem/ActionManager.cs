﻿using System;
using System.Linq;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    /// <summary>
    /// Occurs when an actor is selected.
    /// </summary>
    public event Action<Pawn> OnActorSelected;
    /// <summary>
    /// Occurs when an actor is deselected.
    /// </summary>
    public event Action<Pawn> OnActorDeselected;
    /// <summary>
    /// Occurs when any battle action is selected.
    /// </summary>
    public event Action<BattleAction> OnActionSelected;
    /// <summary>
    /// Occurs when the currently selected battle action is
    /// deselected.
    /// </summary>
    public event Action<BattleAction> OnActionDeselected;
    /// <summary>
    /// Occurs when a target for the current battle action
    /// is selected.
    /// </summary>
    public event Action<BattleAction> OnTargetSelected;
    /// <summary>
    /// Occurs when the target for the current battle
    /// action is deslected.
    /// </summary>
    public event Action<BattleAction> OnTargetDeselected;
    /// <summary>
    /// Occurs after any action is successfully completed
    /// </summary>
    public event Action<Pawn, BattleAction> OnActionStarted;
    /// <summary>
    /// Occurs after any action is successfully completed
    /// </summary>
    public event Action<Pawn, BattleAction> OnActionComplete;
    /// <summary>
    /// Gets whether an actor is currently selected.
    /// </summary>
    public bool HasActor { get { return SelectedActor != null; } }
    /// <summary>
    /// Gets whether an actor is currently assembling an action.
    /// </summary>
    public bool HasAction { get { return HasActor && SelectedAction != null; } }
    /// <summary>
    /// Gets whether an actor is currently confirming whether
    /// to do an action at the target position.
    /// </summary>
    public bool HasTarget { get { return HasAction && SelectedAction.TargetCell != null; } }
    /// <summary>
    /// Gets the actor that the action originates from.
    /// </summary>
    public Pawn SelectedActor { get; private set; }
    /// <summary>
    /// Gets the action the actor will perform.
    /// </summary>
    public BattleAction SelectedAction { get; private set; }

    /// <summary>
    /// Selects the given actor - setting it as the originator of
    /// the action.
    /// </summary>
    public void SelectActor(Pawn actor)
    {
        // Deselect current actor if one is selected
        ClearSelectedActor();

        // Select new actor
        SelectedActor = actor;

        // Notify senpai
        if (actor != null)
            OnActorSelected?.Invoke(actor);
    }

    /// <summary>
    /// Selects an action by index that will be performed by the actor.
    /// </summary>
    public void SelectActionByIndex(int index)
    {
        // Get action name from actor
        BattleAction action = SelectedActor.Actions[index];
        SelectAction(action);
    }

    /// <summary>
    /// Selects an action by name to be used by the current actor.
    /// </summary>
    public void SelectActionByName(string name)
    {
        // Create an instance of the action
        BattleAction action = SelectedActor.Actions[name];
        SelectAction(action);
    }

    /// <summary>
    /// Selects the given action to be used by the current actor.
    /// </summary>
    public void SelectAction(BattleAction action)
    {
        SelectedAction = action;
        OnActionSelected?.Invoke(SelectedAction);
    }

    /// <summary>
    /// Sets the target for the current action.
    /// </summary>
    public bool SetTarget(Vector2Int coordinate)
    {
        Cell cell = SelectedActor.Grid.GetCell(coordinate);
        return SetTarget(cell);
    }

    /// <summary>
    /// Sets the target for the current action.
    /// </summary>
    public bool SetTarget(Cell cell)
    {
        bool validTarget = SelectedAction.SetTarget(cell);
        if (validTarget)
            OnTargetSelected?.Invoke(SelectedAction);
        return validTarget;
    }

    /// <summary>
    /// Performs the stored action. Returns true if the action was
    /// successful.
    /// </summary>
    public void DoAction()
    {
        if (SelectedAction.CanDo())
        {
            // Action about to start!
            OnActionStarted?.Invoke(SelectedActor, SelectedAction);

            // Do the action!
            SelectedAction.Do();

            // Notify senpai.
            OnActionComplete?.Invoke(SelectedActor, SelectedAction);
        }
    }

    /// <summary>
    /// Ends the turn of the selected actor and deselects them.
    /// </summary>
    public void EndSelectedActorTurn()
    {
        ClearSelectedActor();

        TurnManager.Instance.RequestTurnEnd();
    }

    /// <summary>
    /// Removes the target for the current action.
    /// </summary>
    public void ClearSelectedTarget()
    {
        SelectedAction.DeselectTarget();
        OnTargetDeselected?.Invoke(SelectedAction);
    }

    /// <summary>
    /// Removes the selected action so another can be selected.
    /// </summary>
    public void ClearSelectedAction()
    {
        // Local action so can pass it to event after
        // instance action is cleared.
        BattleAction action = SelectedAction;

        // Reset the state of the action.
        SelectedAction?.DeselectTarget();
        SelectedAction = null;
        
        // Notify senpai
        if (action != null)
            OnActionDeselected?.Invoke(action);
    }

    /// <summary>
    /// Removes the selected actor so another can be selected.
    /// Also removes the selected action.
    /// </summary>
    public void ClearSelectedActor()
    {
        // Local actor so can pass it to event after
        // clearing instance actor.
        Pawn actor = SelectedActor;
        
        // Get rid of instance actor and any action
        // being assembled
        ClearSelectedAction();
        SelectedActor = null;

        // Notify senpai
        if (actor != null)
            OnActorDeselected?.Invoke(actor);
    }
}
