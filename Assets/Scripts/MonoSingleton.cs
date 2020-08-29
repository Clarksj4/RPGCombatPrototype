using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Gets the instance of this singleton
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<T>();
                go.name = instance.GetType().ToString();
            }
            return instance;
        }
    }
    private static T instance;

    protected virtual void Awake()
    {
        // Get yourself, before you wreck yourself
        if (instance == null)
            instance = GetComponent<T>();
    }
}