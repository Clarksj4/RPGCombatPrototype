using System;

public class BattleManager : MonoSingleton<BattleManager>
{
    public int RoundCount { get; private set; } = 1;

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
        ActionManager.Instance.ClearSelectedActor();
        ActionManager.Instance.SelectActor(obj as Actor);
    }

    private void HandleOnRoundEnd()
    {
        RoundCount++;
        TurnManager.Instance.Next();
    }
}
