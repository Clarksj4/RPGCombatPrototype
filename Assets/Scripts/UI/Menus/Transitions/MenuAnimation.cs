using System;
using UnityEngine;

public abstract class MenuAnimation : MonoBehaviour
{
    [SerializeField] protected RectTransform target = null;

    public abstract void Play(bool instant, Action onComplete);
}
