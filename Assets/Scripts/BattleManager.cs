using System;

/// <summary>
/// Singleton responsible 
/// </summary>
public class BattleManager : MonoSingleton<BattleManager>
{
    protected override void Awake()
    {
        base.Awake();

        TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
        TurnManager.Instance.OnRoundEnd += HandleOnRoundEnd;
    }

    private void Start()
    {
        PrioritizedStartManager.Instance.InitializeAll(() => {
            // Start the turn!
            TurnManager.Instance.Next();
        });
    }

    private void HandleOnTurnStart(ITurnBased obj)
    {
        // Clear old actor, get the one whose turn it is.
        ActionManager.Instance.ClearSelectedActor();
        ActionManager.Instance.SelectActor(obj as Actor);
    }

    private void HandleOnRoundEnd()
    {
        // Next round doesn't automatically start - need to
        // explicitly tell turn manager to go to next turn.
        TurnManager.Instance.Next();
    }
}
