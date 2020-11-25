using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Actor))]
    public class ActorAppearance : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer frameImage;
        [SerializeField]
        private SpriteRenderer actorImage;
        [SerializeField]
        private SortingGroup maskSortingGroup;

        private Actor actor;

        private void Awake()
        {
            actor = GetComponent<Actor>();
            actor.OnTeamChanged += HandleOnTeamChanged;

            TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
            TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;
        }

        private void Start()
        {
            RefreshActorImage();
        }

        private void RefreshActorImage()
        {
            actorImage.sprite = SpriteManager.Instance.GetSpriteByName(name);
        }

        private void IncrementSortOrders(int amount)
        {
            frameImage.sortingOrder += amount;
            actorImage.sortingOrder += amount;
            maskSortingGroup.sortingOrder += amount;
        }

        private void ApplySelectionColour()
        {
            frameImage.color = Color.green;
            IncrementSortOrders(1);
        }

        private void ApplyTeamColour()
        {
            frameImage.color = actor.Team.Colour;
            IncrementSortOrders(-1);
        }

        private void HandleOnTurnStart(ITurnBased turnBasedEntity)
        {
            if (actor == turnBasedEntity)
                ApplySelectionColour();
        }

        private void HandleOnTurnEnd(ITurnBased turnBasedEntity)
        {
            if (actor == turnBasedEntity)
                ApplyTeamColour();
        }

        private void HandleOnTeamChanged()
        {
            ApplyTeamColour();
        }
    }
}