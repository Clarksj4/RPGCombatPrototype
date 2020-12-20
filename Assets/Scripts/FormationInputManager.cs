using UnityEngine;

public class FormationInputManager : MonoSingleton<FormationInputManager>
{
    public void OnFormationInput(MonoGrid grid, Vector2Int coordinate)
    {
        // If for some reason the grid is not actually a formation
        // don't proceed
        if (!(grid is Formation))
            return;

        // Convert to formation
        Formation formation = grid as Formation;

        // If tapping again in same position.
        if (ActionManager.Instance.HasTarget &&
            ActionManager.Instance.SelectedAction.TargetFormation == formation &&
            ActionManager.Instance.SelectedAction.TargetPosition == coordinate)
            ActionManager.Instance.DoAction();


        // If we've got an action and just need a target 
        else if (ActionManager.Instance.HasAction)
        {
            // Set target if its valid
            ActionManager.Instance.SetTarget(formation, coordinate);
            //if (ActionManager.Instance.SetTarget(formation, coordinate))
            //    ActionManager.Instance.DoAction();
        }
    }
}
