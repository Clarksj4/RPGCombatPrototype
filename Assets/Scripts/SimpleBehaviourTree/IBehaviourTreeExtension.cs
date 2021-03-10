
using System.Collections.Generic;

namespace SimpleBehaviourTree
{
    public static class IBehaviourTreeExtension
    {
        public static IEnumerable<IBehaviourTreeNode> GetLeafNodes(this IBehaviourTreeNode node)
        {
            if (node is ParentBehaviourTreeNode)
            {
                // If the node has children, recurse through them and find leaf nodes
                ParentBehaviourTreeNode parentNode = node as ParentBehaviourTreeNode;
                foreach (IBehaviourTreeNode child in parentNode.children)
                    foreach (IBehaviourTreeNode descendent in GetLeafNodes(child))
                        yield return descendent;
            }

            // If the node is a leaf, return it.
            else
                yield return node;
        }
    }
}