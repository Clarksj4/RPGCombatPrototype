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

        // If we've got an action and just need a target 
        if (ActionManager.Instance.AssemblingAction)
        {
            // Set target if its valid
            if (ActionManager.Instance.SetTarget(formation, coordinate))
                ActionManager.Instance.DoAction();
        }
    }
}
