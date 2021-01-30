using System;
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
        //[SerializeField]
        //private Image maskSortingGroup = null;

        private Pawn actor = null;

        private void Awake()
        {
            actor = GetComponent<Pawn>();
            actor.OnInitialized += HandleOnInitialized;
            actor.OnTeamChanged += HandleOnTeamChanged;

            TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
            TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;
        }

        private void RefreshActorImage()
        {
            foreach (Image image in actorImages)
                image.sprite = SpriteManager.Instance.GetSpriteByName(name);
        }

        //private void IncrementSortOrders(int amount)
        //{
        //    frameImage.sortingOrder += amount;
        //    actorImage.sortingOrder += amount;
        //    maskSortingGroup.sortingOrder += amount;
        //}

        private void ApplySelectionColour()
        {
            frameImage.color = Color.green;
            foreach (var highlight in highlights)
                highlight.SetActive(true);
            //IncrementSortOrders(1);
        }

        private void ApplyTeamColour()
        {
            frameImage.color = actor.Team.Colour;
            foreach (var highlight in highlights)
                highlight.SetActive(false);
            //IncrementSortOrders(-1);
        }

        private void HandleOnTurnStart(ITurnBased turnBasedEntity)
        {
            if (turnBasedEntity == (ITurnBased)actor)
                ApplySelectionColour();
        }

        private void HandleOnTurnEnd(ITurnBased turnBasedEntity)
        {
            if (turnBasedEntity == (ITurnBased)actor)
                ApplyTeamColour();
        }

        private void HandleOnInitialized(Pawn pawn)
        {
            RefreshActorImage();
        }

        private void HandleOnTeamChanged()
        {
            ApplyTeamColour();
        }
    }
}