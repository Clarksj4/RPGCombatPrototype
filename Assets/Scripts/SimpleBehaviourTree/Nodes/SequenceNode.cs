
namespace SimpleBehaviourTree
{
    /// <summary>
    /// Runs child nodes in sequence, until one fails.
    /// </summary>
    public class SequenceNode : ParentBehaviourTreeNode
    {
        public override bool Do(BehaviourTreeState state)
        {
            // Run child nodes in sequence until one fails.
            foreach (var child in children)
            {
                var childStatus = child.Do(state);
                if (childStatus != true)
                    return childStatus;
            }

            return true;
        }
    }
}
