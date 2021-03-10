
namespace SimpleBehaviourTree
{
    /// <summary>
    /// Selects the first node that succeeds. Tries successive nodes until it finds one that doesn't fail.
    /// </summary>
    public class SelectorNode : ParentBehaviourTreeNode
    {
        public override bool Do(BehaviourTreeState state)
        {
            // Select the first node that succeeds. Tries successive nodes 
            // until it finds one that doesn't fail.
            foreach (var child in children)
            {
                var childStatus = child.Do(state);
                if (childStatus != false)
                    return childStatus;
            }

            return false;
        }
    }
}
