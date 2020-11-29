using DG.Tweening;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour, IStartable
{
    [SerializeField][Range(1, 10)] private int nVisibleActors = 4;
    [SerializeField] private bool horizontal;
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private LayoutGroup slideyBit;
    [SerializeField] private TurnOrderUIFrame FramePrefab;

    //Properties
    private float FrameSize {  get { return horizontal ? FrameSizeDelta.y : FrameSizeDelta.x; } }
    private Vector2 FrameSizeDelta { get { return rectTransform.sizeDelta; } }
    private float Spacing { get { return horizontal ? (slideyBit as HorizontalLayoutGroup).spacing : (slideyBit as VerticalLayoutGroup).spacing; } }

    // Components
    private RectTransform slideyBitRectTransform;
    private RectTransform rectTransform;

    // Fields
    private Sequence slideSequence;

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
            AddFrame(actor, true);
            yield return null;
        }

        UpdateSize();
        PrioritizedStartManager.Instance.MarkInitializationComplete(this);
    }

    private void HandleOnTurnEnd(ITurnBased entity)
    {
        // If the last sequence is still in progress - force
        // it to be complete so we can start the next one.
        if (slideSequence != null && 
            slideSequence.active)
            slideSequence.Complete(true);
            
        // Get the frame of the actor whose turn it just was.
        TurnOrderUIFrame lastActorFrame = GetLastFrame();

        // Duplicate the frame at the start of the order.
        AddFrame(lastActorFrame.Actor, true);

        // Slide the order along to hide the actor who just went
        slideSequence = DOTween.Sequence();

        if (horizontal)
            slideSequence.Append(slideyBitRectTransform.DOAnchorPosX(FrameSize, slideDuration));
        else
            slideSequence.Append(slideyBitRectTransform.DOAnchorPosY(FrameSize, slideDuration));

        slideSequence.AppendCallback(() => {
            // Get rid of the original previous actor's frame 
            // and reset the position of the slider.
            DestroyImmediate(lastActorFrame.gameObject);
            slideyBitRectTransform.anchoredPosition = Vector2.zero;

        });
    }

    private void AddFrame(Actor actor, bool first = false)
    {
        // Create as first child
        TurnOrderUIFrame frame = Instantiate(FramePrefab, slideyBit.transform, false);
        frame.RectTransform.sizeDelta = Vector2.one * FrameSize;
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
        // Show nVisibleActors or as many as there are if there is not enough.
        int actorCount = Mathf.Min(slideyBit.transform.childCount, nVisibleActors);

        // Calculate total width of panel
        float sumFrameSizes = FrameSize * actorCount;
        float sumSpacing = Spacing * (actorCount - 1);
        float totalSize = sumFrameSizes + sumSpacing;

        if (horizontal)
            rectTransform.sizeDelta = new Vector2(totalSize, rectTransform.sizeDelta.y);

        else
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, totalSize);
    }
}
