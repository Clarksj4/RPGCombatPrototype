using UnityEngine;
using System.Collections;

public class CancelBar : Menu
{
    public void OnCancelTapped()
    {
        ActionManager.Instance.ClearSelectedAction();
        MenuStack.Instance.CloseCurrent();
    }
}
