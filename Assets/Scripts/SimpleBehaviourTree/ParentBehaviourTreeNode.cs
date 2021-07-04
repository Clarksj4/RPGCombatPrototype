using Sirenix.Serialization;
using System;
using System.Collections.Generic;

namespace SimpleBehaviourTree
{
    /// <summary>
    /// Interface for behaviour tree nodes.
    /// </summary>
    [Serializable]
    public abstract class ParentBehaviourTreeNode : IBehaviourTreeNode
    {
        /// <summary>
        /// List of child nodes.
        /// </summary>
        [OdinSerialize]
        public List<IBehaviourTreeNode> children = new List<IBehaviourTreeNode>();

        public abstract bool Do(Blackboard state);
    }
}
