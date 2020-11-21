using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RoundAnnouncement : MonoBehaviour
{
    [SerializeField]
    private Image background;
    [SerializeField]
    private Text text;
    [SerializeField]
    private float stallRange = 40;
    private float alpha;
    

    private float OffScreenX { get { return (background.rectTransform.rect.width / 2) + (text.rectTransform.rect.width / 2); } }

    private void Awake()
    {
        alpha = background.color.a;

        TurnManager.Instance.OnRoundStart += HandleOnRoundStart;
    }

    private void HandleOnRoundStart()
    {
        AnimateIn(() => {
            AnimateStall(() => {
                AnimateOut(null);
            });
        });
    }

    private void AnimateIn(Action onComplete)
    {
        background.color = new Color(0, 0, 0, 0);

        text.text = $"Round {TurnManager.Instance.RoundCount}";
        text.rectTransform.anchoredPosition = new Vector2(-OffScreenX, 0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(background.DOFade(alpha, 0.5f));
        sequence.Join(text.rectTransform.DOAnchorPosX(-stallRange / 2, 0.3f));
        sequence.InsertCallback(0.3f, () => { onComplete?.Invoke(); });
        
        sequence.Play();
    }

    private void AnimateStall(Action onComplete)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(text.rectTransform.DOAnchorPosX(stallRange / 2, 1).SetEase(Ease.Flash));
        sequence.AppendCallback(() => { onComplete?.Invoke(); });
        sequence.Play();
    }

    private void AnimateOut(Action onComplete)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(text.rectTransform.DOAnchorPosX(OffScreenX, 0.3f));
        sequence.Join(background.DOFade(0, 0.5f));
        sequence.AppendInterval(0.5f);

        sequence.AppendCallback(() => { onComplete?.Invoke(); });
        sequence.Play();
    }
}
