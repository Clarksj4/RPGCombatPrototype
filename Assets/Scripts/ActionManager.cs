using System;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    public bool AssemblingAction { get { return selectedAction != null; } }

    private Actor selectedActor;
    private BattleAction selectedAction;

    public void SelectActor(Actor actor)
    {
        selectedActor = actor;
    }

    public void SelectAction<TBattleAction>() where TBattleAction : BattleAction
    {
        // Create an instance of the action
        selectedAction = (BattleAction)Activator.CreateInstance(typeof(TBattleAction));
        selectedAction.SetActor(selectedActor);
    }

    public bool IsValidTarget(BattleMap map, Vector2Int target)
    {

        return selectedAction.IsValidTarget(map, target);
    }

    public void SetTarget(BattleMap map, Vector2Int target)
    {
        selectedAction.SetTarget(map, target);
    }

    public void DoAction()
    {
        selectedAction.Do();
        EndSelectedActorTurn();
    }

    public void EndSelectedActorTurn()
    {
        selectedActor.EndTurn();
        ClearSelectedActor();
    }

    public void ClearSelectedAction()
    {
        selectedAction = null;
    }

    public void ClearSelectedActor()
    {
        ClearSelectedAction();
        selectedActor = null;
    }
}
