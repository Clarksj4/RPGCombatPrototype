using System;
using UnityEngine;
using UnityEngine.UI;

public class PawnUI : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    private Pawn pawn;

    private void Awake()
    {
        pawn = GetComponentInParent<Pawn>();
        pawn.OnHealthChanged += HandleOnPawnHealthChanged;
    }

    private void HandleOnPawnHealthChanged(int change)
    {
        float fill = (float)pawn.Health / pawn.MaxHealth;
        healthBar.fillAmount = fill;
    }
}
