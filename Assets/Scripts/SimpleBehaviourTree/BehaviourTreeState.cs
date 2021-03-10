using System.Collections;
using System.Collections.Generic;

namespace SimpleBehaviourTree
{
    public class BehaviourTreeState : IEnumerable
    {
        /// <summary>
        /// Gets or sets the data with the given name. Returns
        /// null if no data with the given name exists.
        /// </summary>
        public object this[string name]
        {
            get 
            {
                // Return it if it exists, otherwise return null.
                if (state.ContainsKey(name))
                    return state[name];
                return null;
            }

            set
            {
                // Set it if it exists.
                if (state.ContainsKey(name))
                    state[name] = value;

                // Add it if it doesn't exist.
                else
                    Add(name, value);
            }
        }

        private Dictionary<string, object> state = new Dictionary<string, object>();

        public T Get<T>(string name)
        {
            object data = this[name];
            if (data != null)
                return (T)data;
            return default(T);
        }

        public void Add(string name, object data)
        {
            state.Add(name, data);
        }

        public bool Remove(string name)
        {
            return state.Remove(name);
        }

        public IEnumerator GetEnumerator()
        {
            return state.GetEnumerator();
        }
    }
}