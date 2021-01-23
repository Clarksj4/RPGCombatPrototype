
public class SummonNode : ActionNode
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public float Priority { get; set; }

    public override bool Do()
    {
        SummonManager.Instance.Spawn(Name, Target, Priority, Duration);
        return true;
    }
}
