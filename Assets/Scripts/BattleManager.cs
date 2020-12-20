using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Singleton responsible 
/// </summary>
public class BattleManager : MonoSingleton<BattleManager>
{
    public MonoGrid Grid { get; private set; }
    public Formation[] Formations { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Grid = FindObjectOfType<MonoGrid>();
        Formations = FindObjectsOfType<Formation>();

        TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
        TurnManager.Instance.OnRoundEnd += HandleOnRoundEnd;
    }

    private void Start()
    {
        PrioritizedStartManager.Instance.InitializeAll(() => {
            // TODO: assign things to teams!
            int i = 1;
            foreach(Formation formation in Formations)
            {
                TeamManager.Instance.AddTeamAndMembers
                (
                    $"Team {i}", 
                    i == 1 ? Color.blue : Color.magenta, 
                    formation.Pawns.Select(p => p as ITeamBased)
                );
                i++;
            }
            
            
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
