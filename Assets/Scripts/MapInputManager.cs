using UnityEngine;
using System.Collections;

public class MapInputManager : Singleton<MapInputManager>
{
    public void OnMapInput(BattleMap map, Vector2Int coordinate)
    {
        if (!ActionManager.Instance.AssemblingAction)
        {
            // Select actor at input position if there is one
            Pawn pawn = map.GetPawnAtCoordinate(coordinate);
            if (pawn != null && pawn is Actor)
            {
                Actor actor = pawn as Actor;
                ActionManager.Instance.SelectActor(actor);

                // Close all menus and open this actor's actions bar
                MenuStack.Instance.HideAll();
                MenuStack.Instance.Show<ActionsBar>();
            }
        }
            
        else
        {
            // Set target if its valid
            if (ActionManager.Instance.SetTarget(map, coordinate))
            {
                ActionManager.Instance.DoAction();
                MenuStack.Instance.HideAll();
            }
        }
    }
}
