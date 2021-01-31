
public class SummonNode : ActionNode
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public float Priority { get; set; }

    public override bool Do(Pawn actor, Cell target)
    {
        SummonManager.Instance.Spawn(Name, target, Priority, Duration);
        return true;
    }
}
