using System;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    /// <summary>
    /// Occurs after any action is successfully completed
    /// </summary>
    public event Action<Actor, BattleAction> OnAfterSuccessfulAction;
    /// <summary>
    /// Gets whether an actor is currently assembling an action.
    /// </summary>
    public bool AssemblingAction { get { return SelectedAction != null; } }
    /// <summary>
    /// Gets the actor that the action originates from.
    /// </summary>
    public Actor SelectedActor { get; private set; }
    /// <summary>
    /// Gets the action the actor will perform.
    /// </summary>
    public BattleAction SelectedAction { get; private set; }

    /// <summary>
    /// Selects the given actor - setting it as the originator of
    /// the action.
    /// </summary>
    public void SelectActor(Actor actor)
    {
        SelectedActor = actor;
    }

    /// <summary>
    /// Selects the action, by name, that will be performed by the actor.
    /// </summary>
    public void SelectAction(string name)
    {
        // Create an instance of the action
        SelectedAction = (BattleAction)Activator.CreateInstance(Type.GetType(name));
        SelectedAction.SetActor(SelectedActor);
    }

    /// <summary>
    /// Selects the action, by type, that will be performed by the actor.
    /// </summary>
    /// <typeparam name="TBattleAction"></typeparam>
    public void SelectAction<TBattleAction>() 
        where TBattleAction : BattleAction
    {
        // Create an instance of the action
        SelectedAction = (BattleAction)Activator.CreateInstance(typeof(TBattleAction));
        SelectedAction.SetActor(SelectedActor);
    }

    /// <summary>
    /// Sets the target for the current action.
    /// </summary>
    public bool SetTarget(BattleMap map, Vector2Int target)
    {
        return SelectedAction.SetTarget(map, target);
    }

    /// <summary>
    /// Performs the stored action. Returns true if the action was
    /// successful.
    /// </summary>
    public bool DoAction()
    {
        // Try do the action
        bool done = SelectedAction.Do();
        if (done)
        {
            // If it was successful, notify senpai, and end actors turn.
            OnAfterSuccessfulAction?.Invoke(SelectedActor, SelectedAction);
            EndSelectedActorTurn();
        }
            
        return done;
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
    /// Removes the selected action so another can be selected.
    /// </summary>
    public void ClearSelectedAction()
    {
        SelectedAction = null;
    }

    /// <summary>
    /// Removes the selected actor so another can be selected.
    /// Also removes the selected action.
    /// </summary>
    public void ClearSelectedActor()
    {
        ClearSelectedAction();
        SelectedActor = null;
    }
}
