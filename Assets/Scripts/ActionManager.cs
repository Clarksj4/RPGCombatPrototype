using System;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    public bool AssemblingAction { get { return SelectedAction != null; } }
    public Actor SelectedActor { get; private set; }
    public BattleAction SelectedAction { get; private set; }

    public void SelectActor(Actor actor)
    {
        SelectedActor = actor;
    }

    public void SelectAction<TBattleAction>() where TBattleAction : BattleAction
    {
        // Create an instance of the action
        SelectedAction = (BattleAction)Activator.CreateInstance(typeof(TBattleAction));
        SelectedAction.SetActor(SelectedActor);
    }

    public bool SetTarget(BattleMap map, Vector2Int target)
    {
        return SelectedAction.SetTarget(map, target);
    }

    public bool DoAction()
    {
        bool done = SelectedAction.Do();
        if (done)
            EndSelectedActorTurn();
        return done;
    }

    public void EndSelectedActorTurn()
    {
        SelectedActor.EndTurn();
        ClearSelectedActor();
    }

    public void ClearSelectedAction()
    {
        SelectedAction = null;
    }

    public void ClearSelectedActor()
    {
        ClearSelectedAction();
        SelectedActor = null;
    }
}
