
using SimpleBehaviourTree;
using UnityEngine;

public class DoDamageNode : ActionNode
{
    [Tooltip("The amount of damage this action will do.")]
    public int BaseDamage;
    [Tooltip("Whether the damage is amplified by the actor's power level.")]
    public bool Amplifyable = true;
    [Tooltip("Whether this damage can be reduced by defense or delegated to a surrogate.")]
    public bool Defendable = true;

    public override bool Do(Blackboard state)
    {
        Pawn actor = state.Get<Pawn>("Actor");
        Pawn defender = state.Get<Cell>("Cell")
                            ?.GetContent<Pawn>();
        
        // If there is a defender - do damage to it
        if (defender != null)
        {
            int attack = Amplifyable ? actor.GetAmplifiedDamage(BaseDamage) : BaseDamage;
            int inflicted = defender.TakeDamage(attack, Defendable);

            // Update state with damage
            state["Attack"] = state.Get<int>("Attack") + attack;
            state["Inflicted"] = state.Get<int>("Inflicted") + inflicted;

            return inflicted > 0;
        }
        
        // Only counts as success if damage was inflicted
        return false;
    }
}
