
namespace SimpleBehaviourTree
{
    /// <summary>
    /// Interface for behaviour tree nodes.
    /// </summary>
    public interface IBehaviourTreeNode
    {
        /// <summary>
        /// Action method for this node.
        /// </summary>
        bool Do(Blackboard state);
    }
}
