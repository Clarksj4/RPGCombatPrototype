public interface IStartable
{
    /// <summary>
    /// Initializes this startable - method should return true
    /// if initialization is completed within this method call.
    /// Otherwise, false should be returned and
    /// PrioritizedStartManager.MarkInitializationComplete()
    /// should be called once this object is initialized.
    /// </summary>
    bool Initialize();
}