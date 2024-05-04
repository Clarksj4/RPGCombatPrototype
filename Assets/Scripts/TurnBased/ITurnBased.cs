public interface ITurnBased
{
    int Priority { get; }
    void OnTurnStart();
    void OnTurnEnd();
}
