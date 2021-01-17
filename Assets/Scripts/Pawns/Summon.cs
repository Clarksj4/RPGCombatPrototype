
public class Summon : Pawn, ITurnBased
{
    public float Priority { get; private set; }

    public int Duration { get; set; }

    public void Setup(float priority, int duration)
    {
        Priority = priority;
        TurnManager.Instance.Add(this);
        TurnManager.Instance.OnTurnStart += HandleOnTurnStart;

        Duration = duration;
    }

    private void HandleOnTurnStart(ITurnBased obj)
    {
        if (obj == this)
        {
            // Automatically go to the next turn in order.
            TurnManager.Instance.Next();

            print($"{name}'s turn! Remaining duration: {Duration}");
            // Reduce duration and kill self if expired.
            Duration -= 1;
            if (Duration <= 0)
            {
                Destroy();
                TurnManager.Instance.Remove(this);
            }
        }
    }
}
