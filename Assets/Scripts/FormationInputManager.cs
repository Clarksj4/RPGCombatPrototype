using UnityEngine;

public class FormationInputManager : MonoSingleton<FormationInputManager>
{
    public void OnFormationInput(Grid grid, Vector2Int coordinate)
    {
        // If for some reason the grid is not actually a formation
        // don't proceed
        if (!(grid is Formation))
            return;

        // Convert to formation
        Formation formation = grid as Formation;

        if (!ActionManager.Instance.AssemblingAction)
        {
            // Select actor at input position if there is one
            Pawn pawn = formation.GetPawnAtCoordinate(coordinate);
            if (pawn != null && pawn is Actor)
            {
                Actor actor = pawn as Actor;
                ActionManager.Instance.SelectActor(actor);
            }
        }
            
        else
        {
            // Set target if its valid
            if (ActionManager.Instance.SetTarget(formation, coordinate))
                ActionManager.Instance.DoAction();
        }
    }
}
