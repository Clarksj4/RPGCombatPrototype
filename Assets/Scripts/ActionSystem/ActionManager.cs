using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ActionManager : MonoSingleton<ActionManager>
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

    private Coroutine actionCoroutine;

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
    /// Selects the action, by name, that will be performed by the actor.
    /// </summary>
    public void SelectAction(int index)
    {
        // Get action name from actor
        string name = SelectedActor.Actions[index];
        SelectAction(name);
    }

    public void SelectAction(string name)
    {
        // Create an instance of the action
        BattleAction action = CreateAction(name + "Action");
        SelectAction(action);
    }

    public void SelectAction(BattleAction action)
    {
        // Create an instance of the action
        SelectedAction = action;
        OnActionSelected?.Invoke(SelectedAction);
    }

    /// <summary>
    /// Checks if the current actor can perform the action
    /// with the given name.
    /// </summary>
    public bool CanDo(string actionName)
    {
        BattleAction action = CreateAction(actionName);
        return CanDo(action);
    }

    /// <summary>
    /// Checks if the current actor can perform the
    /// given action.
    /// </summary>
    public bool CanDo(BattleAction action)
    {
        return action.CanDo() &&
               action.TargetableCells.Count() > 0;
    }

    public BattleAction CreateAction(string actionName)
    {
        // Create an instance of the action
        BattleAction action = (BattleAction)Activator.CreateInstance(Type.GetType(actionName));
        action.SetActor(SelectedActor);
        return action;

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
            actionCoroutine = StartCoroutine(DoActionInternal());
    }

    /// <summary>
    /// Ends the turn of the selected actor and deselects them.
    /// </summary>
    public void EndSelectedActorTurn()
    {
        ClearSelectedActor();

        TurnManager.Instance.Next();
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

    private IEnumerator DoActionInternal()
    {
        // Action about to start!
        OnActionStarted?.Invoke(SelectedActor, SelectedAction);

        // Do the action then remove coroutine ref.
        yield return SelectedAction.Do();
        actionCoroutine = null;

        // Notify senpai.
        OnActionComplete?.Invoke(SelectedActor, SelectedAction);
    }
}
