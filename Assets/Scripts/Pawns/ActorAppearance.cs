using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Assets.Scripts
{
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
            actor.OnStatusApplied += HandleOnStatusApplied;
            actor.OnStatusExpired += HandleOnStatusExpired;

            ActionManager.Instance.OnActionStarted += HandleOnActionStarted;

            TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
            TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;
        }


        private void RefreshStats()
        {
            defenseText.text = actor.Defense.ToString();
            manaText.text = actor.Mana.ToString();
        }

        private void RefreshActorImage()
        {
            foreach (Image image in actorImages)
                image.sprite = actor.Stats.HeadSprite;
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

        private void HandleOnStatusExpired(PawnStatus obj)
        {
            RefreshStats();
        }

        private void HandleOnStatusApplied(PawnStatus obj)
        {
            RefreshStats();
        }
    }
}