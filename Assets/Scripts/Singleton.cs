using System;

public class Singleton<T>
{
    /// <summary>
    /// Gets the instance of this singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = Activator.CreateInstance<T>();
            return instance;
        }
    }
    private static T instance;
}