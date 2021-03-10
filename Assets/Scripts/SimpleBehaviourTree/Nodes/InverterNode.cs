using System.Linq;

namespace SimpleBehaviourTree
{
    /// <summary>
    /// Decorator node that inverts the success/failure of its child.
    /// </summary>
    public class InverterNode : ParentBehaviourTreeNode
    {
        public override bool Do(BehaviourTreeState state)
        {
            // Return the opposite result
            var result = children.Single().Do(state);
            return !result;
        }
    }
}
