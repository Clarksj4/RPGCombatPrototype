using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private bool empties = true;

    [Header("Components")]
    [SerializeField] private Image frontBar = null;
    [SerializeField] private Image backBar = null;

    [Header("Fill Animation")]
    [SerializeField] private Color fillColour = default;
    [SerializeField] private Ease fillEasing = Ease.Unset;
    [SerializeField] private float fillDuration = 0f;

    [Header("Drain Animation")]
    [SerializeField] private Color drainColour = default;
    [SerializeField] private Ease drainEasing = Ease.Unset;
    [SerializeField] private float drainDuration = 0f;

    private float currentHealth = 1f;
    private float currentFill = 1f;

    public void SetHealth(float amount, bool animate = true)
    {
        bool filling = amount > currentHealth;
        currentHealth = amount;
        currentFill = empties ? amount : 1 - amount;
        if (!animate)
        {
            frontBar.fillAmount = currentFill;
            backBar.fillAmount = currentFill;
        }

        else if (filling)
        {
            if (empties)
                DoFillAnimation();
            else
                DoDrainAnimation();
        }

        else
        {
            if (empties)
                DoDrainAnimation();
            else
                DoFillAnimation();
        }
    }

    private void DoFillAnimation()
    {
        // TODO: set backBar colour to fillColour
        backBar.color = fillColour;

        // TODO: immediately set backBar to currentAmount
        //backBar.fillAmount = currentFill;

        // TODO: animate frontBar to currentAmount over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(backBar.DOFillAmount(currentFill, fillDuration / 40f).SetEase(fillEasing));
        sequence.Append(frontBar.DOFillAmount(currentFill, fillDuration).SetEase(fillEasing));
    }

    private void DoDrainAnimation()
    {
        // TODO: set backBar colour to drainColour
        backBar.color = drainColour;

        // TODO: immediately set fontBar to currentAmount
        //frontBar.fillAmount = currentFill;

        // TODO: animate backBar to currentAmount over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(frontBar.DOFillAmount(currentFill, drainDuration / 40f).SetEase(drainEasing));
        sequence.Append(backBar.DOFillAmount(currentFill, drainDuration).SetEase(drainEasing));
    }
}
