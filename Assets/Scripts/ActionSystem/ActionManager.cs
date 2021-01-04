using System;
using System.Collections;
using UnityEngine;

public class ActionManager : MonoSingleton<ActionManager>
{
    /// <summary>
    /// Occurs when an actor is selected.
    /// </summary>
    public event Action<Actor> OnActorSelected;
    /// <summary>
    /// Occurs when an actor is deselected.
    /// </summary>
    public event Action<Actor> OnActorDeselected;
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
    public event Action<Actor, BattleAction> OnActionStarted;
    /// <summary>
    /// Occurs after any action is successfully completed
    /// </summary>
    public event Action<Actor, BattleAction> OnActionComplete;
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
    public bool HasTarget { get { return HasAction && SelectedAction.TargetFormation != null; } }
    /// <summary>
    /// Gets the actor that the action originates from.
    /// </summary>
    public Actor SelectedActor { get; private set; }
    /// <summary>
    /// Gets the action the actor will perform.
    /// </summary>
    public BattleAction SelectedAction { get; private set; }

    private Coroutine actionCoroutine;

    /// <summary>
    /// Selects the given actor - setting it as the originator of
    /// the action.
    /// </summary>
    public void SelectActor(Actor actor)
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
    public void SelectAction(string name)
    {
        // Create an instance of the action
        SelectedAction = (BattleAction)Activator.CreateInstance(Type.GetType(name));
        SelectedAction.SetActor(SelectedActor);

        OnActionSelected?.Invoke(SelectedAction);
    }

    /// <summary>
    /// Selects the action, by type, that will be performed by the actor.
    /// </summary>
    public void SelectAction<TBattleAction>() 
        where TBattleAction : BattleAction
    {
        // Create an instance of the action
        SelectedAction = (BattleAction)Activator.CreateInstance(typeof(TBattleAction));
        SelectedAction.SetActor(SelectedActor);

        OnActionSelected?.Invoke(SelectedAction);
    }

    /// <summary>
    /// Sets the target for the current action.
    /// </summary>
    public bool SetTarget(MonoGrid grid, Vector2Int target)
    {
        bool validTarget = SelectedAction.SetTarget(grid, target);
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
        SelectedActor.EndTurn();
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
        Actor actor = SelectedActor;
        
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

        // Notify senpai, and end actors turn.
        OnActionComplete?.Invoke(SelectedActor, SelectedAction);
        EndSelectedActorTurn();
    }
}
