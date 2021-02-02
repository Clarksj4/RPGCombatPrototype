public interface ITurnBased
{
    float Priority { get; }

    void OnTurnStart();
    void OnTurnEnd();
}
