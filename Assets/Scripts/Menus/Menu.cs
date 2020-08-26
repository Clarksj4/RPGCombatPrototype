using UnityEngine;
using DG.Tweening;
using System;

public abstract class Menu : MonoBehaviour
{
    /// <summary>
    /// Occurs just before this menu is shown.
    /// </summary>
    public event Action<Menu> OnBeforeShow;
    /// <summary>
    /// Occurs after this menu has completed its 'show' transition.
    /// </summary>
    public event Action<Menu> OnShown;
    /// <summary>
    /// Occurs just before this menu is closed.
    /// </summary>
    public event Action<Menu> OnBeforeHide;
    /// <summary>
    /// Occurs after this menu has completed its 'hide' transition
    /// </summary>
    public event Action<Menu> OnHidden;

    [Tooltip("The movement this menu will do when transitioning from on screen to off screen.")]
    public Vector2 Transition;
    [Tooltip("The duration in seconds of the transition")]
    public float Duration;

    private Canvas parentCanvas;
    private new RectTransform transform;
    private Vector2 onScreenPosition;
    private Vector2 offScreenPosition;

    private Sequence showSequence;
    private Sequence hideSequence;

    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        
        transform = GetComponent<RectTransform>();
        onScreenPosition = transform.anchoredPosition;
        offScreenPosition = onScreenPosition + Transition;

        MenuStack.Instance.RegisterMenu(this);

        Hide(true);
    }

    protected virtual void PreShow() { /* Nothing! */ }
    protected virtual void PostShow() { /* Nothing! */ }
    protected virtual void PreHide() { /* Nothing! */ }
    protected virtual void PostHide() { /* Nothing! */ }

    public virtual void Show(bool instant = false)
    {
        PreShow();

        OnBeforeShow?.Invoke(this);
        KillSequences();
        parentCanvas.gameObject.SetActive(true);

        if (instant)
        {
            transform.anchoredPosition = onScreenPosition;
            OnShown?.Invoke(this);
            PostShow();
        }

        else
        {
            showSequence = DOTween.Sequence();
            showSequence.Append(transform.DOAnchorPos(onScreenPosition, Duration)
                                         .SetEase(Ease.OutBack));
            showSequence.AppendCallback(() => { 
                OnShown?.Invoke(this);
                PostShow();
            });
            showSequence.Play();
        }
    }

    public virtual void Hide(bool instant = false)
    {
        PreHide();

        OnBeforeHide?.Invoke(this);
        KillSequences();

        if (instant)
        {
            transform.anchoredPosition = offScreenPosition;
            parentCanvas.gameObject.SetActive(false);
            OnShown?.Invoke(this);
            PostHide();
        }

        else
        {
            hideSequence = DOTween.Sequence();
            hideSequence.Append(transform.DOAnchorPos(offScreenPosition, Duration)
                                         .SetEase(Ease.OutQuad));
            hideSequence.AppendCallback(() => {
                parentCanvas.gameObject.SetActive(false);
                OnHidden?.Invoke(this);
                PostHide();
            });
            hideSequence.Play();
        }
    }

    private void KillSequences()
    {
        showSequence?.Kill();
        hideSequence?.Kill();
    }
}
