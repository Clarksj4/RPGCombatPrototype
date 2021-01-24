
public class HasCellContentNode : ActionNode
{
    public TargetableCellContent Content { get; set; }

    public override bool Do()
    {
        Pawn defender = Target.GetContent<Pawn>();
        if (defender != null)
            return defender.HasStatus();

        // Fudge it if there is no defender
        return true;
    }
}
