using UnityEngine;
using System.Collections;

public class MapInputManager : Singleton<MapInputManager>
{
    public void OnMapInput(BattleMap map, Vector2Int coordinate)
    {
        if (!ActionManager.Instance.AssemblingAction)
        {
            // Select actor at input position if there is one
            Actor actor = map.GetActorAtCoordinate(coordinate);
            if (actor != null)
            {
                ActionManager.Instance.SelectActor(actor);
                MenuStack.Instance.Show<ActionsBar>();
            }
        }
            
        else
        {
            // Select target if its valid
            if (ActionManager.Instance.IsValidTarget(map, coordinate))
            {
                ActionManager.Instance.SetTarget(map, coordinate);
                ActionManager.Instance.DoAction();
                MenuStack.Instance.CloseAll();
            }
        }
    }
}
