using DG.Tweening;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour, IStartable
{
    [SerializeField]
    private HorizontalLayoutGroup slideyBit;
    [SerializeField]
    private TurnOrderUIFrame FramePrefab;

    private RectTransform slideyBitRectTransform;
    private RectTransform rectTransform;

    private void Awake()
    {
        slideyBitRectTransform = slideyBit.transform as RectTransform;
        rectTransform = transform as RectTransform;

        TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;
        PrioritizedStartManager.Instance.RegisterWithPriority(this, 1);
    }

    public bool Initialize()
    {
        StartCoroutine(DoInitialize());
        return false;
    }

    private IEnumerator DoInitialize()
    {
        foreach (ITurnBased turnbased in TurnManager.Instance.OrderOfActors)
        {
            Actor actor = turnbased as Actor;
            AddFrame(actor);
            yield return null;
        }

        UpdateSize();
        PrioritizedStartManager.Instance.MarkInitializationComplete(this);
    }

    private void HandleOnTurnEnd(ITurnBased entity)
    {
        // TODO: add a new frame for the current actor
        TurnOrderUIFrame lastActorFrame = GetLastFrame();

        // TODO: add it as first sibling
        AddFrame(lastActorFrame.Actor, true);

        // TODO: slide bit along 60 units
        Sequence slideSequence = DOTween.Sequence();
        slideSequence.Append(slideyBitRectTransform.DOAnchorPosX(60, 1f));
        slideSequence.AppendCallback(() => {
            // Remove the excess frame and snap the transform back
            Destroy(lastActorFrame.gameObject);
            slideyBitRectTransform.anchoredPosition = Vector2.zero;
        });
    }

    private void AddFrame(Actor actor, bool first = false)
    {
        // Create as first child
        TurnOrderUIFrame frame = Instantiate(FramePrefab, slideyBit.transform, false);
        if (first)
            frame.transform.SetAsFirstSibling();

        // Set actor
        frame.SetActor(actor);
    }

    private TurnOrderUIFrame GetLastFrame()
    {
        int childIndex = slideyBit.transform.childCount - 1;
        Transform lastChild = slideyBit.transform.GetChild(childIndex);
        TurnOrderUIFrame lastActorFrame = lastChild.GetComponent<TurnOrderUIFrame>();
        return lastActorFrame;
    }

    private void UpdateSize()
    {
        int actorCount = slideyBit.transform.childCount;
        float frameWidth = FramePrefab.RectTransform.sizeDelta.x;
        float sumFrameWidths = frameWidth * actorCount;
        float sumSpacing = slideyBit.spacing * (actorCount - 1);
        float totalWidth = sumFrameWidths + sumSpacing;

        rectTransform.sizeDelta = new Vector2(totalWidth, rectTransform.sizeDelta.y);
    }
}
