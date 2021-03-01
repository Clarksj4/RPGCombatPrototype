using UnityEngine;
using UnityEngine.UI;

public class PawnPortrait : MonoBehaviour
{
    [SerializeField]
    private Image frameImage = null;
    [SerializeField]
    private Image[] actorImages = null;
    [SerializeField]
    private GameObject[] highlights = null;

    public void HandleOnTurnStart(Pawn pawn)
    {
        ApplySelectionColour();
    }

    public void HandleOnTurnEnd(Pawn pawn)
    {
        ApplyTeamColour(pawn);
    }

    public void HandleOnInitialized(Pawn pawn)
    {
        RefreshPawnImage(pawn);
    }

    public void HandleOnTeamChanged(Pawn pawn)
    {
        ApplyTeamColour(pawn);
    }

    private void RefreshPawnImage(Pawn pawn)
    {
        foreach (Image image in actorImages)
            image.sprite = pawn.Data.HeadSprite;
    }

    private void ApplySelectionColour()
    {
        frameImage.color = Color.green;
        foreach (var highlight in highlights)
            highlight.SetActive(true);
    }

    private void ApplyTeamColour(Pawn pawn)
    {
        frameImage.color = pawn.Team.Colour;
        foreach (var highlight in highlights)
            highlight.SetActive(false);
    }
}
