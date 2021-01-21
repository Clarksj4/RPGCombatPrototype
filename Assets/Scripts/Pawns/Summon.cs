
using UnityEngine;

public class Summon : MonoBehaviour
{
    public int Duration { get; set; }
    public Pawn Pawn { get; private set; }

    private void Awake()
    {
        Pawn = GetComponent<Pawn>();
    }

    public void Setup(int duration, float priority)
    {
        Duration = duration;
        Pawn.Priority = priority;

        TurnManager.Instance.UpdatePosition(Pawn);
        TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;
    }

    private void HandleOnTurnEnd(ITurnBased ent)
    {
        if (ent == (ITurnBased)Pawn)
        {
            // Reduce duration and kill self if expired.
            Duration -= 1;
            if (Duration <= 0)
                Pawn.Destroy();
        }
    }
}
