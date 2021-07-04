using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBehaviourTree
{
    public class Blackboard : IEnumerable
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
                if (items.ContainsKey(name))
                    return items[name];
                return null;
            }

            set
            {
                // Set it if it exists.
                if (items.ContainsKey(name))
                    items[name] = value;

                // Add it if it doesn't exist.
                else
                    Add(name, value);
            }
        }

        private Dictionary<string, object> items = new Dictionary<string, object>();

        /// <summary>
        /// Adds data with the given name.
        /// </summary>
        public void Add(string name, object data)
        {
            items.Add(name, data);
        }

        /// <summary>
        /// Gets the data with the given name. Returns
        /// the default value for the given type if no data
        /// exists.
        /// </summary>
        public T Get<T>(string name)
        {
            object data = this[name];
            if (data != null)
                return (T)data;

            // Can't return null on generic param
            return default(T);
        }

        /// <summary>
        /// Gets all items in the blackboard with the
        /// given type.
        /// </summary>
        public IEnumerable<T> GetAll<T>()
        {
            return items.Values
                        .Where(i => i is T)
                        .Select(i => (T)i);
        }

        /// <summary>
        /// Checks if there is any items in the blackboard 
        /// of the given type.
        /// </summary>
        public bool Any<T>()
        {
            foreach (object item in items)
            {
                if (item is T)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the data with the given name if
        /// it exists. Returns true if the data was
        /// present and removed.
        /// </summary>
        public bool Remove(string name)
        {
            return items.Remove(name);
        }

        public IEnumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}