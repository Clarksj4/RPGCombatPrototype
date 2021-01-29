using UnityEngine;

public class TargetCommand : Command
{
    public Vector2Int Coordinate { get; set; }

    public override void Do()
    {
        if (ActionManager.Instance.HasAction)
            ActionManager.Instance.SetTarget(Coordinate);
    }
}