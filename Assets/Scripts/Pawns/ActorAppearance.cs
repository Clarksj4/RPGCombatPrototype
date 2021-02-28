using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Pawn))]
public class ActorAppearance : MonoBehaviour
{
    [SerializeField]
    private Image frameImage = null;
    [SerializeField]
    private Image[] actorImages = null;
    [SerializeField]
    private GameObject[] highlights = null;
    [SerializeField]
    private TextMeshProUGUI defenseText = null;
    [SerializeField]
    private TextMeshProUGUI manaText = null;

    private Pawn actor = null;

    private void Awake()
    {
        actor = GetComponent<Pawn>();
        actor.OnInitialized += HandleOnInitialized;
        actor.OnTeamChanged += HandleOnTeamChanged;

        ActionManager.Instance.OnActionStarted += HandleOnActionStarted;

        TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
        TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;
    }

    public void RefreshStats()
    {
        defenseText.text = actor.Stats["Defense"].Value.ToString();
        manaText.text = actor.Stats["Mana"].Value.ToString();
    }

    public void RefreshActorImage()
    {
        //foreach (Image image in actorImages)
        //    image.sprite = actor.Stats.HeadSprite;
    }

    private void ApplySelectionColour()
    {
        frameImage.color = Color.green;
        foreach (var highlight in highlights)
            highlight.SetActive(true);
    }

    private void ApplyTeamColour()
    {
        frameImage.color = actor.Team.Colour;
        foreach (var highlight in highlights)
            highlight.SetActive(false);
    }

    private void HandleOnTurnStart(ITurnBased turnBasedEntity)
    {
        if (turnBasedEntity == (ITurnBased)actor)
        {
            ApplySelectionColour();
            RefreshStats();
        }
    }

    private void HandleOnTurnEnd(ITurnBased turnBasedEntity)
    {
        if (turnBasedEntity == (ITurnBased)actor)
        {
            ApplyTeamColour();
            RefreshStats();
        }
    }

    private void HandleOnInitialized(Pawn pawn)
    {
        RefreshActorImage();
    }

    private void HandleOnTeamChanged()
    {
        ApplyTeamColour();
    }

    private void HandleOnActionStarted(Pawn pawn, BattleAction action)
    {
        if (pawn == actor)
            RefreshStats();
    }
}
