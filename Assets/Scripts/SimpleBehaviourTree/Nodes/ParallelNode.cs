
namespace SimpleBehaviourTree
{
    /// <summary>
    /// Runs childs nodes in parallel.
    /// </summary>
    public class ParallelNode : ParentBehaviourTreeNode
    {
        /// <summary>
        /// Number of child successess require to terminate with success.
        /// </summary>
        public int NumRequiredToSucceed { get; protected set; }

        public override bool Do(BehaviourTreeState state)
        {
            var numChildrenSuceeded = 0;
            var numChildrenFailed = 0;

            foreach (var child in children)
            {
                var childStatus = child.Do(state);
                if (childStatus == true) 
                    ++numChildrenSuceeded;
                else
                    ++numChildrenFailed;
            }

            if (NumRequiredToSucceed > 0 && 
                numChildrenSuceeded >= NumRequiredToSucceed)
                return true;

            return false;
        }
    }
}
