using UnityEngine;

public class MonoGridInputManager : MonoSingleton<MonoGridInputManager>
{
    public void OnCellInput(MonoGrid grid, Cell cell)
    {
        // If tapping again in same position.
        if (ActionManager.Instance.HasTarget &&
            ActionManager.Instance.SelectedAction.TargetPosition == cell.Coordinate)
            ActionManager.Instance.DoAction();


        // If we've got an action and just need a target 
        else if (ActionManager.Instance.HasAction)
        {
            // Set target if its valid
            ActionManager.Instance.SetTarget(grid, cell.Coordinate);
            //if (ActionManager.Instance.SetTarget(formation, coordinate))
            //    ActionManager.Instance.DoAction();
        }
    }
}
