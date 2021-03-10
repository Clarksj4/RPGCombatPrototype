
using SimpleBehaviourTree;

public class DoDamageNode : ActionNode
{
    /// <summary>
    /// Gets or sets the amount of damage this action will do.
    /// </summary>
    public int BaseDamage;
    /// <summary>
    /// Gets or sets whether the damage is amplified by the
    /// actor's power level.
    /// </summary>
    public bool Amplifyable = true;
    /// <summary>
    /// Gets or sets whether this damage can be reduced by
    /// defense or delegated to a surrogate.
    /// </summary>
    public bool Defendable = true;

    public override bool Do(BehaviourTreeState state)
    {
        Pawn actor = state.Get<Pawn>("Actor");
        Pawn defender = state.Get<Cell>("Cell")
                            ?.GetContent<Pawn>();
        
        // If there is a defender - do damage to it
        if (defender != null)
        {
            int attack = Amplifyable ? actor.GetAmplifiedDamage(BaseDamage) : BaseDamage;
            int inflicted = defender.TakeDamage(attack, Defendable);
            return inflicted > 0;
        }
        
        // Only counts as success if damage was inflicted
        return false;
    }
}
