using DG.Tweening;
using System;
using UnityEngine;

public class SlideMenuAnimation : MenuAnimation
{
    [Tooltip("Relative position where the target will slide to")]
    [SerializeField] private Vector2 to = new Vector2(0, -80);
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private Ease ease = Ease.Linear;

    private Vector2 startPosition;

    private void Awake()
    {
        startPosition = target.anchoredPosition;
    }

    public override void Play(bool instant = false, Action onComplete = null)
    {
        target.DOKill();

        // Calculate the end position here incase the serialized values have
        // changed at runtime.
        Vector2 endPosition = startPosition + to;

        if (instant)
            target.anchoredPosition = endPosition;

        else
        {
            target.DOAnchorPos(endPosition, duration)
                  .OnComplete(() => onComplete?.Invoke())
                  .SetEase(ease);
        }
    }
}
