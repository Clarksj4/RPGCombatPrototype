using UnityEngine;
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

    [Header("Menu Transition")]
    [SerializeField] private MenuAnimation showAnimation = null;
    [SerializeField] private MenuAnimation hideAnimation = null;

    private Canvas parentCanvas;

    protected virtual void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        MenuStack.Instance.RegisterMenu(this);
    }

    protected virtual void Start()
    {
        Hide(true);
    }

    protected virtual void PreShow() { /* Nothing! */ }
    protected virtual void PostShow() { /* Nothing! */ }
    protected virtual void PreHide() { /* Nothing! */ }
    protected virtual void PostHide() { /* Nothing! */ }

    public virtual void Show(bool instant = false)
    {
        // Execute pre show logic
        PreShow();
        OnBeforeShow?.Invoke(this);

        // Show the canvas so this menu will be visible during transition
        parentCanvas.gameObject.SetActive(true);

        if (showAnimation != null)
        {
            // Play the animation
            showAnimation.Play(instant, () => {
                // Execute post show logic
                OnShown?.Invoke(this);
                PostShow();
            });
        }

        else
        {
            // Execute post show logic
            OnShown?.Invoke(this);
            PostShow();
        }
    }

    public virtual void Hide(bool instant = false)
    {
        // Execute pre hide logic
        PreHide();
        OnBeforeHide?.Invoke(this);

        if (hideAnimation != null)
        {
            // Play the animation
            hideAnimation.Play(instant, () => {
                // Hide the canvas so this menu is no longer visible.
                parentCanvas.gameObject.SetActive(false);

                // Execute post hide logic
                OnHidden?.Invoke(this);
                PostHide();
            });
        }

        else
        {
            // Hide the canvas so this menu is no longer visible.
            parentCanvas.gameObject.SetActive(false);

            // Execute post hide logic
            OnHidden?.Invoke(this);
            PostHide();
        }
    }
}
