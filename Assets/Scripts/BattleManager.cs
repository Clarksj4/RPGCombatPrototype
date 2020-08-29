public class BattleManager : MonoSingleton<BattleManager>
{
    public int RoundCount { get; private set; } = 1;

    protected override void Awake()
    {
        base.Awake();

        TurnManager.Instance.OnRoundEnd += HandleOnRoundEnd;
    }

    private void Start()
    {
        PrioritizedStartManager.Instance.InitializeAll(() => {
            // Start the turn!
            TurnManager.Instance.Next();
        });
    }

    private void HandleOnRoundEnd()
    {
        RoundCount++;
        TurnManager.Instance.Next();
    }
}
