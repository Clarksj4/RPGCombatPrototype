using UnityEngine;

public class FormationInputManager : MonoSingleton<FormationInputManager>
{
    public void OnFormationInput(MonoGrid grid, Vector2Int coordinate)
    {
        // If tapping again in same position.
        if (ActionManager.Instance.HasTarget &&
            ActionManager.Instance.SelectedAction.TargetPosition == coordinate)
            ActionManager.Instance.DoAction();


        // If we've got an action and just need a target 
        else if (ActionManager.Instance.HasAction)
        {
            // Set target if its valid
            ActionManager.Instance.SetTarget(grid, coordinate);
            //if (ActionManager.Instance.SetTarget(formation, coordinate))
            //    ActionManager.Instance.DoAction();
        }
    }
}
