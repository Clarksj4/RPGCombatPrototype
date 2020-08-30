using UnityEngine;

public class MapInputManager : MonoSingleton<MapInputManager>
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
            }
        }
            
        else
        {
            // Set target if its valid
            if (ActionManager.Instance.SetTarget(map, coordinate))
                ActionManager.Instance.DoAction();
        }
    }
}
