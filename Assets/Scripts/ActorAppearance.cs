using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Actor))]
    public class ActorAppearance : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer frameImage;
        [SerializeField]
        private SpriteRenderer actorImage;

        private Actor actor;

        private void Awake()
        {
            actor = GetComponent<Actor>();

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

        private void HandleOnTurnStart(ITurnBased turnBasedEntity)
        {
            if (actor == turnBasedEntity)
                frameImage.color = Color.green;
        }

        private void HandleOnTurnEnd(ITurnBased turnBasedEntity)
        {
            if (actor == turnBasedEntity)
                frameImage.color = Color.white;
        }
    }
}